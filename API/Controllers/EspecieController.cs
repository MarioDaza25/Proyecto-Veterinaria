using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
    public async Task<ActionResult<Pager<EspecieDto>>> Get([FromQuery] Params especieParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Especies.GetAllAsync(especieParams.PageIndex,especieParams.PageSize,especieParams.Search);
        var listaEspecies = _mapper.Map<List<EspecieDto>>(registros);
        return new Pager<EspecieDto>(listaEspecies,totalRegistros,especieParams.PageIndex,especieParams.PageSize,especieParams.Search);
    }
}
