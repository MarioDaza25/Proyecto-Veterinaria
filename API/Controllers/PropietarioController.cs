using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
    public async Task<ActionResult<Pager<PropietarioDto>>> Get([FromQuery] Params propParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Propietarios.GetAllAsync(propParams.PageIndex,propParams.PageSize,propParams.Search);
        var listaPropi = _mapper.Map<List<PropietarioDto>>(registros);
        return new Pager<PropietarioDto>(listaPropi,totalRegistros,propParams.PageIndex,propParams.PageSize,propParams.Search);
    }
}