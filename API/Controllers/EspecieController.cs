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
[Authorize(Roles = "Empleado, Administrador, Gerente")]
public class EspecieController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EspecieController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EspecieDto>>> Get()
    {
        var especies = await _unitOfWork.Especies.GetAllAsync();
        return _mapper.Map<List<EspecieDto>>(especies);
    }


    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<EspecieDto>>> Get([FromQuery] Params especieParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Especies.GetAllAsync(especieParams.PageIndex,especieParams.PageSize,especieParams.Search);
        var listaEspecies = _mapper.Map<List<EspecieDto>>(registros);
        return new Pager<EspecieDto>(listaEspecies,totalRegistros,especieParams.PageIndex,especieParams.PageSize,especieParams.Search);
    }


    //Mascotas que se encuentren registradas cuya especie sea felina.
    [HttpGet("ConMascotas/{especie}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EspecieXRazaDto>>> MascotaXUnaEspecie(string especie)
    {
        if (especie == "")
        {
            return BadRequest("Ingrese una Especie.");
        }
        var mascotas = await _unitOfWork.Especies.MascotaXUnaEspecie(especie);
        return _mapper.Map<List<EspecieXRazaDto>>(mascotas);
    } 

    //Listar todas las mascotas agrupadas por especie..
    [HttpGet("ConMascotas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EspecieXRazaDto>>> MascotaXEspecie()
    {
        var mascotas = await _unitOfWork.Especies.MascotaXEspecie();
        return _mapper.Map<List<EspecieXRazaDto>>(mascotas);
    }
    
    
    [HttpPost]
    [Authorize(Roles = "Empleado")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(EspeciePDto especieDto)
    {
        var especie = _mapper.Map<Especie>(especieDto);
        _unitOfWork.Especies.Add(especie);
        await _unitOfWork.SaveAsync();
        if (especie == null)
        {
            return BadRequest();
        }
        return "Especie Creada con Éxito!";
    }

    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] EspeciePDto especieDto)
    {
        if (especieDto == null|| id != especieDto.Id)
        {
            return BadRequest();
        }
        var especieExistente = await _unitOfWork.Especies.GetByIdAsync(id);

        if (especieExistente == null)
        {
            return NotFound();
        }
        _mapper.Map(especieDto, especieExistente);
        _unitOfWork.Especies.Update(especieExistente);
        await _unitOfWork.SaveAsync();

        return "Especie Actualizada con Éxito!";
    } 
     


    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var especie = await _unitOfWork.Especies.GetByIdAsync(id);
        if (especie == null)
        {
            return NotFound();
        }
        _unitOfWork.Especies.Remove(especie);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"La Especie {especie.Nombre} se eliminó con éxito." });
    }
}
