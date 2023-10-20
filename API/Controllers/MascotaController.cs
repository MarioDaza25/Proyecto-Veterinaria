using API.Dtos;
using API.Dtos.DtosPost;
using API.Helpers;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
[Authorize(Roles = "Empleado, Administrador, Gerente")]
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
    public async Task<ActionResult<IEnumerable<MascotaNombreDto>>> Get()
    {
        var mascotas = await _unitOfWork.Mascotas.GetAllAsync();
        return _mapper.Map<List<MascotaNombreDto>>(mascotas);
    }


    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MascotaDto>>> Get([FromQuery] Params MascotaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Mascotas.GetAllAsync(MascotaParams.PageIndex,MascotaParams.PageSize,MascotaParams.Search);
        var listaMascota = _mapper.Map<List<MascotaDto>>(registros);
        return new Pager<MascotaDto>(listaMascota,totalRegistros,MascotaParams.PageIndex,MascotaParams.PageSize,MascotaParams.Search);
    }

    //Listar las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023
    [HttpGet("AtendidasXMotivo/{motivo}/{anio}/{trimestre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MascotaNombreDto>>> MascotasAtendidasXMotivoXtrimestre(string motivo, int anio, int trimestre)
    {
        var movimiento = await _unitOfWork.Mascotas.MascotasAtendidasXMotivoXtrimestre(trimestre,anio,motivo);
        return _mapper.Map<List<MascotaNombreDto>>(movimiento);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(MascotaPDto mascotaDto)
    {
        var mascota = _mapper.Map<Mascota>(mascotaDto);
        _unitOfWork.Mascotas.Add(mascota);
        await _unitOfWork.SaveAsync();
        if (mascota == null)
        {
            return BadRequest();
        }
        return "Mascota Creada con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] TipMovPDto mascotaDto)
    {
        if (mascotaDto == null|| id != mascotaDto.Id)
        {
            return BadRequest();
        }
        var mascota = await _unitOfWork.Mascotas.GetByIdAsync(id);

        if (mascota == null)
        {
            return NotFound();
        }
        _mapper.Map(mascotaDto, mascota);
        _unitOfWork.Mascotas.Update(mascota);
        await _unitOfWork.SaveAsync();

        return "Mascota Actualizada con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var mascota = await _unitOfWork.Mascotas.GetByIdAsync(id);
        if (mascota == null)
        {
            return NotFound();
        }
        _unitOfWork.Mascotas.Remove(mascota);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"La Mascota {mascota.Nombre} se eliminó con éxito." });
    }
    
}