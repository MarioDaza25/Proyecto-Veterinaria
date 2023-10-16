using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
    public async Task<ActionResult<Pager<TipoMovDto>>> Get([FromQuery] Params aParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.TipoMovimientos.GetAllAsync(aParams.PageIndex,aParams.PageSize,aParams.Search);
        var lista = _mapper.Map<List<TipoMovDto>>(registros);
        return new Pager<TipoMovDto>(lista,totalRegistros,aParams.PageIndex,aParams.PageSize,aParams.Search);
    }
}