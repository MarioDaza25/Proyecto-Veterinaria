using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class RazaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RazaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<RazaDto>>> Get([FromQuery] Params razaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Razas.GetAllAsync(razaParams.PageIndex,razaParams.PageSize,razaParams.Search);
        var listaRazas = _mapper.Map<List<RazaDto>>(registros);
        return new Pager<RazaDto>(listaRazas,totalRegistros,razaParams.PageIndex,razaParams.PageSize,razaParams.Search);
    }
}