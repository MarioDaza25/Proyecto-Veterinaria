using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RolController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RolController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<RolDto>>> Get([FromQuery] Params aParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Roles.GetAllAsync(aParams.PageIndex,aParams.PageSize,aParams.Search);
        var lista = _mapper.Map<List<RolDto>>(registros);
        return new Pager<RolDto>(lista,totalRegistros,aParams.PageIndex,aParams.PageSize,aParams.Search);
    }
}