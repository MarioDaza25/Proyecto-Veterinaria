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
    public async Task<ActionResult<IEnumerable<RazaDto>>> Get()
    {
        var razas = await _unitOfWork.Razas.GetAllAsync();
        return _mapper.Map<List<RazaDto>>(razas);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<RazaDto>>> Get([FromQuery] Params razaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Razas.GetAllAsync(razaParams.PageIndex,razaParams.PageSize,razaParams.Search);
        var listaRazas = _mapper.Map<List<RazaDto>>(registros);
        return new Pager<RazaDto>(listaRazas,totalRegistros,razaParams.PageIndex,razaParams.PageSize,razaParams.Search);
    }

     //Listar las mascotas y sus propietarios cuya raza sea Golden Retriver
    [HttpGet("MascotaConPropietario/{raza}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MascotaXRazaDto>>> MascotasxRaza(string raza)
    {
        if (raza == "")
        {
            return BadRequest("Ingrese una Especialidad.");
        }
        var razas = await _unitOfWork.Razas.MascotasxRaza(raza);
        return _mapper.Map<List<MascotaXRazaDto>>(razas);
    } 

    //Listar la cantidad de mascotas que pertenecen a una raza
    [HttpGet("TotalMascotasPorRaza")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TotalMascotasXRazaDto>>> CantidadMascotasXRaza()
    {
        var razas = await _unitOfWork.Razas.CantidadMascotasXRaza();
        return _mapper.Map<List<TotalMascotasXRazaDto>>(razas);
    } 

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(RazaPDto razaDto)
    {
        var raza = _mapper.Map<Raza>(razaDto);
        _unitOfWork.Razas.Add(raza);
        await _unitOfWork.SaveAsync();
        if (raza == null)
        {
            return BadRequest();
        }
        return "Raza Creada con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] RazaPDto razaDto)
    {
        if (razaDto == null || id != razaDto.Id)
        {
            return BadRequest();
        }
        var razaExiste = await _unitOfWork.Razas.GetByIdAsync(id);

        if (razaExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(razaDto, razaExiste);
        _unitOfWork.Razas.Update(razaExiste);
        await _unitOfWork.SaveAsync();

        return "Raza Actualizada con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var raza = await _unitOfWork.Razas.GetByIdAsync(id);
        if (raza == null)
        {
            return NotFound();
        }
        _unitOfWork.Razas.Remove(raza);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"La Raza {raza.Nombre} se eliminó con éxito." });
    }
}