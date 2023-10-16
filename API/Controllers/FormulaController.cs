using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class FormulaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public FormulaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<FormulaDto>>> Get([FromQuery] Params formulaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.FormulasMedicas.GetAllAsync(formulaParams.PageIndex,formulaParams.PageSize,formulaParams.Search);
        var listaFormulas = _mapper.Map<List<FormulaDto>>(registros);
        return new Pager<FormulaDto>(listaFormulas,totalRegistros,formulaParams.PageIndex,formulaParams.PageSize,formulaParams.Search);
    }
}
