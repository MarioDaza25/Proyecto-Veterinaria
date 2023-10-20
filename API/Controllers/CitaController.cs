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
public class CitaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CitaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [Authorize(Roles = "Empleado")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CitaDto>>> Get()
    {
        var citas = await _unitOfWork.Citas.GetAllAsync();
        return _mapper.Map<List<CitaDto>>(citas);
    }

    
    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CitaDto>>> Get([FromQuery] Params citaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Citas.GetAllAsync(citaParams.PageIndex,citaParams.PageSize,citaParams.Search);
        var listaCitas = _mapper.Map<List<CitaDto>>(registros);
        return new Pager<CitaDto>(listaCitas,totalRegistros,citaParams.PageIndex,citaParams.PageSize,citaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(CitaPDto citaDto)
    {
        var cita = _mapper.Map<Cita>(citaDto);
        _unitOfWork.Citas.Add(cita);
        await _unitOfWork.SaveAsync();
        if (cita == null)
        {
            return BadRequest();
        }
        return "Cita Creada con Éxito!";
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] CitaPDto citaDto)
    {
        if (citaDto == null|| id != citaDto.Id)
        {
            return BadRequest();
        }
        var citaExistente = await _unitOfWork.Citas.GetByIdAsync(id);

        if (citaExistente == null)
        {
            return NotFound();
        }
        _mapper.Map(citaDto, citaExistente);
        _unitOfWork.Citas.Update(citaExistente);
        await _unitOfWork.SaveAsync();

        return "Cita Actualizada con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var cita = await _unitOfWork.Citas.GetByIdAsync(id);
        if (cita == null)
        {
            return NotFound();
        }
        _unitOfWork.Citas.Remove(cita);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"La cita {id} se eliminó con éxito." });
    }
}


