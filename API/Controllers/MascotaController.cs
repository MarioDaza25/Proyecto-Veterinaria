using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

public class MascotaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public MascotaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MascotaDto>>> Get([FromQuery] Params MascotaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Mascotas.GetAllAsync(MascotaParams.PageIndex,MascotaParams.PageSize,MascotaParams.Search);
        var listaMascota = _mapper.Map<List<MascotaDto>>(registros);
        return new Pager<MascotaDto>(listaMascota,totalRegistros,MascotaParams.PageIndex,MascotaParams.PageSize,MascotaParams.Search);
    }
    
}