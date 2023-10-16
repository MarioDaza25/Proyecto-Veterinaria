using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
    public async Task<ActionResult<Pager<MovimientoDto>>> Get([FromQuery] Params movParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Movimientos.GetAllAsync(movParams.PageIndex,movParams.PageSize,movParams.Search);
        var listaMov = _mapper.Map<List<MovimientoDto>>(registros);
        return new Pager<MovimientoDto>(listaMov,totalRegistros,movParams.PageIndex,movParams.PageSize,movParams.Search);
    }
}