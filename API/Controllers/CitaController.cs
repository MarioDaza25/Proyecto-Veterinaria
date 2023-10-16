using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CitaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CitaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [Authorize(Roles = "Empleado")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CitaDto>>> Get([FromQuery] Params citaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Citas.GetAllAsync(citaParams.PageIndex,citaParams.PageSize,citaParams.Search);
        var listaCitas = _mapper.Map<List<CitaDto>>(registros);
        return new Pager<CitaDto>(listaCitas,totalRegistros,citaParams.PageIndex,citaParams.PageSize,citaParams.Search);
    }


    

}
