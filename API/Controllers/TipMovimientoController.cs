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
public class TipMovimientoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TipMovimientoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoMovDto>>> Get()
    {
        var tipMovim = await _unitOfWork.TipoMovimientos.GetAllAsync();
        return _mapper.Map<List<TipoMovDto>>(tipMovim);
    }


    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TipoMovDto>>> Get([FromQuery] Params aParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.TipoMovimientos.GetAllAsync(aParams.PageIndex,aParams.PageSize,aParams.Search);
        var lista = _mapper.Map<List<TipoMovDto>>(registros);
        return new Pager<TipoMovDto>(lista,totalRegistros,aParams.PageIndex,aParams.PageSize,aParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(TipMovPDto tipMovDto)
    {
        var tipoMovimiento = _mapper.Map<TipoMovimiento>(tipMovDto);
        _unitOfWork.TipoMovimientos.Add(tipoMovimiento);
        await _unitOfWork.SaveAsync();
        if (tipoMovimiento == null)
        {
            return BadRequest();
        }
        return "Tipo Movimiento Creado con Éxito!";
    }

    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] TipMovPDto tipMovDto)
    {
        if (tipMovDto == null|| id != tipMovDto.Id)
        {
            return BadRequest();
        }
        var TipMovExiste = await _unitOfWork.TipoMovimientos.GetByIdAsync(id);

        if (TipMovExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(tipMovDto, TipMovExiste);
        _unitOfWork.TipoMovimientos.Update(TipMovExiste);
        await _unitOfWork.SaveAsync();

        return "Tipo Movimiento Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var tipoMovimiento = await _unitOfWork.TipoMovimientos.GetByIdAsync(id);
        if (tipoMovimiento == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoMovimientos.Remove(tipoMovimiento);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Tipo de Movimiento {tipoMovimiento.Descripcion} se eliminó con éxito." });
    }
}