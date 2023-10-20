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
public class PropietarioController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PropietarioController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PropietarioDto>>> Get()
    {
        var propietarios = await _unitOfWork.Propietarios.GetAllAsync();
        return _mapper.Map<List<PropietarioDto>>(propietarios);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PropietarioDto>>> Get([FromQuery] Params propParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Propietarios.GetAllAsync(propParams.PageIndex,propParams.PageSize,propParams.Search);
        var listaPropi = _mapper.Map<List<PropietarioDto>>(registros);
        return new Pager<PropietarioDto>(listaPropi,totalRegistros,propParams.PageIndex,propParams.PageSize,propParams.Search);
    }


    //Listar los propietarios y sus mascotas.
    [HttpGet("ConMascotas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PropConMascotaDto>>> PropietarioConMascotas()
    {
        var propietario = await _unitOfWork.Propietarios.PropietarioConMascotas();
        return _mapper.Map<List<PropConMascotaDto>>(propietario);
    } 

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(PropietarioPDto propietarioDto)
    {
        var propietario = _mapper.Map<Propietario>(propietarioDto);
        _unitOfWork.Propietarios.Add(propietario);
        await _unitOfWork.SaveAsync();
        if (propietario == null)
        {
            return BadRequest();
        }
        return "Propietario Creado con Éxito!";
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] PropietarioPDto propietarioDto)
    {
        if (propietarioDto == null|| id != propietarioDto.Id)
        {
            return BadRequest();
        }
        var propietario = await _unitOfWork.Propietarios.GetByIdAsync(id);

        if (propietario == null)
        {
            return NotFound();
        }
        _mapper.Map(propietarioDto, propietario);
        _unitOfWork.Propietarios.Update(propietario);
        await _unitOfWork.SaveAsync();

        return "Propietario Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var propietario = await _unitOfWork.Propietarios.GetByIdAsync(id);
        if (propietario == null)
        {
            return NotFound();
        }
        _unitOfWork.Propietarios.Remove(propietario);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Propietario {propietario.Nombre} se eliminó con éxito." });
    }

    
}