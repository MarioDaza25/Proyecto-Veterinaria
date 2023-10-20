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
public class MovimientoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public MovimientoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MovimientoDto>>> Get()
    {
        var movimientos = await _unitOfWork.Movimientos.GetAllAsync();
        return _mapper.Map<List<MovimientoDto>>(movimientos);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MovimientoDto>>> Get([FromQuery] Params movParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Movimientos.GetAllAsync(movParams.PageIndex,movParams.PageSize,movParams.Search);
        var listaMov = _mapper.Map<List<MovimientoDto>>(registros);
        return new Pager<MovimientoDto>(listaMov,totalRegistros,movParams.PageIndex,movParams.PageSize,movParams.Search);
    }


    //Listar todos los movimientos de medicamentos y el valor total de cada movimiento.
    [HttpGet("ValorTotal")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MovimientosConTotalDto>>> ResumenMovimientos()
    {
        var movimiento = await _unitOfWork.Movimientos.ResumenMovimientos();
        return _mapper.Map<List<MovimientosConTotalDto>>(movimiento);
    } 



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(MovimientoPDto movimientoDto)
    {
        movimientoDto.DetalleMovimientos.ForEach(id => id.Id_Movimiento = movimientoDto.Id);

        var movimiento = _mapper.Map<Movimiento>(movimientoDto);
        _unitOfWork.Movimientos.Add(movimiento);
         await _unitOfWork.SaveAsync();
         
        return "Movimiento Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] MovimientoPDto movimientoDto)
    {
        if (movimientoDto == null || id != movimientoDto.Id)
        {
            return BadRequest();
        }
        var movimiento = await _unitOfWork.Movimientos.GetByIdAsync(id);

        if (movimiento == null)
        {
            return NotFound();
        }
        _mapper.Map(movimientoDto, movimiento);
        _unitOfWork.Movimientos.Update(movimiento);
        await _unitOfWork.SaveAsync();

        return "Movimiento Actualizado con Éxito!";
    } 


    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var movimiento = await _unitOfWork.Movimientos.GetByIdAsync(id);
        if (movimiento == null)
        {
            return NotFound();
        }
        _unitOfWork.Movimientos.Remove(movimiento);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Movimiento {id} se eliminó con éxito." });
    }
}