using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProveedorController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProveedorController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProveedorDto>>> Get([FromQuery] Params ProveedParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Proveedores.GetAllAsync(ProveedParams.PageIndex,ProveedParams.PageSize,ProveedParams.Search);
        var listaProv = _mapper.Map<List<ProveedorDto>>(registros);
        return new Pager<ProveedorDto>(listaProv,totalRegistros,ProveedParams.PageIndex,ProveedParams.PageSize,ProveedParams.Search);
    }
}