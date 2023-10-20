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
public class VeterinarioController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public VeterinarioController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VeterinarioDto>>> Get()
    {
        var veterinarios = await _unitOfWork.Veterinarios.GetAllAsync();
        return _mapper.Map<List<VeterinarioDto>>(veterinarios);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<VeterinarioDto>>> Get([FromQuery] Params aParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Veterinarios.GetAllAsync(aParams.PageIndex,aParams.PageSize,aParams.Search);
        var lista = _mapper.Map<List<VeterinarioDto>>(registros);
        return new Pager<VeterinarioDto>(lista,totalRegistros,aParams.PageIndex,aParams.PageSize,aParams.Search);
    }

    //Veterinarios cuya especialidad sea Cirujano vascular.
    [HttpGet("Especialidad/{especialidad}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VeterinarioDto>>> VeterinariosXEspecilidad(string especialidad)
    {
        if (especialidad == "")
        {
            return BadRequest("Ingrese una Especialidad.");
        }
        var veterinario = await _unitOfWork.Veterinarios.VeterinariosXEspecilidad(especialidad);
        return _mapper.Map<List<VeterinarioDto>>(veterinario);
    } 

    //Listar las mascotas que fueron atendidas por un determinado veterinario.
    [HttpGet("ConMascotasAtendidas/{nombreVeterinario}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VeterConMascoDto>>> MascotaXVeterinario(string nombreVeterinario)
    {
        if (nombreVeterinario == "")
        {
            return BadRequest("Ingrese el Nombre de un Veterinario.");
        }
        var veterinarios = await _unitOfWork.Veterinarios.MascotaXVeterinario(nombreVeterinario);
        return _mapper.Map<List<VeterConMascoDto>>(veterinarios);
    } 

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(VeterinarioPDto veterinarioDto)
    {
        var veterinario = _mapper.Map<Veterinario>(veterinarioDto);
        _unitOfWork.Veterinarios.Add(veterinario);
        await _unitOfWork.SaveAsync();
        if (veterinario == null)
        {
            return BadRequest();
        }
        return "Veterinario Creado con Éxito!";
    }

    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] VeterinarioPDto veterinarioDto)
    {
        if (veterinarioDto == null|| id != veterinarioDto.Id)
        {
            return BadRequest();
        }
        var veteExistente = await _unitOfWork.Veterinarios.GetByIdAsync(id);

        if (veteExistente == null)
        {
            return NotFound();
        }
        _mapper.Map(veterinarioDto, veteExistente);
        _unitOfWork.Veterinarios.Update(veteExistente);
        await _unitOfWork.SaveAsync();

        return "Veterinario Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var veterinario = await _unitOfWork.Veterinarios.GetByIdAsync(id);
        if (veterinario == null)
        {
            return NotFound();
        }
        _unitOfWork.Veterinarios.Remove(veterinario);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Veterinario {veterinario.Nombre} se eliminó con éxito." });
    }
}