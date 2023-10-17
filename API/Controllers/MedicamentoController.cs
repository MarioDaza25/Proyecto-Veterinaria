using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MedicamentoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public MedicamentoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MedicamentoDto>>> Get([FromQuery] Params MedParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Medicamentos.GetAllAsync(MedParams.PageIndex,MedParams.PageSize,MedParams.Search);
        var listaMed = _mapper.Map<List<MedicamentoDto>>(registros);
        return new Pager<MedicamentoDto>(listaMed,totalRegistros,MedParams.PageIndex,MedParams.PageSize,MedParams.Search);
    }

    //Medicamentos que tenga un precio de venta mayor a 50000
    [HttpGet("PrecioMayorA/{precio}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicaNombreDto>>> MedicamentoXPrecio(decimal precio)
    {
        var medicamento = await _unitOfWork.Medicamentos.MedicamentoXPrecio(precio);
        return _mapper.Map<List<MedicaNombreDto>>(medicamento);
    } 
}