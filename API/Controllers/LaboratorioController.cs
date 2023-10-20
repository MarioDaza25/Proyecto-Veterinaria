using API.Dtos;
using API.Dtos.DtosPost;
using API.Helpers;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class LaboratorioController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public LaboratorioController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<LaboratorioDto>>> Get()
    {
        var laboratorios = await _unitOfWork.Laboratorios.GetAllAsync();
        return _mapper.Map<List<LaboratorioDto>>(laboratorios);
    }


    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<LaboratorioDto>>> Get([FromQuery] Params labParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Laboratorios.GetAllAsync(labParams.PageIndex,labParams.PageSize,labParams.Search);
        var listaLab = _mapper.Map<List<LaboratorioDto>>(registros);
        return new Pager<LaboratorioDto>(listaLab,totalRegistros,labParams.PageIndex,labParams.PageSize,labParams.Search);
    }

    //Medicamentos que pertenezcan a el laboratorio Genfar
    [HttpGet("ConMedicamentos/{laboratorio}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<LabxMedicaDto>>> MedicamentoXLaboratorio(string laboratorio)
    {
        if (laboratorio == "")
        {
            return BadRequest("Ingrese un laboratorio.");
        }
        var medicamento = await _unitOfWork.Laboratorios.MedicamentoXLaboratorio(laboratorio);
        return _mapper.Map<List<LabxMedicaDto>>(medicamento);
    } 

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(LaboratorioPDto laboratDto)
    {
        var laboratorio = _mapper.Map<Laboratorio>(laboratDto);
        _unitOfWork.Laboratorios.Add(laboratorio);
        await _unitOfWork.SaveAsync();
        if (laboratorio == null)
        {
            return BadRequest();
        }
        return "Laboratorio Creado con Éxito!";
    }

    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] LaboratorioPDto laboratDto)
    {
        if (laboratDto == null|| id != laboratDto.Id)
        {
            return BadRequest();
        }
        var LaboratExistente = await _unitOfWork.Laboratorios.GetByIdAsync(id);

        if (LaboratExistente == null)
        {
            return NotFound();
        }
        _mapper.Map(laboratDto, LaboratExistente);
        _unitOfWork.Laboratorios.Update(LaboratExistente);
        await _unitOfWork.SaveAsync();

        return "Laboratorio Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var laboratorio = await _unitOfWork.Laboratorios.GetByIdAsync(id);
        if (laboratorio == null)
        {
            return NotFound();
        }
        _unitOfWork.Laboratorios.Remove(laboratorio);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Laboratorio {laboratorio.Nombre} se eliminó con éxito." });
    }
}
