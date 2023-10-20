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
public class RolController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RolController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RolDto>>> Get()
    {
        var roles = await _unitOfWork.Roles.GetAllAsync();
        return _mapper.Map<List<RolDto>>(roles);
    }


    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<RolDto>>> Get([FromQuery] Params aParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Roles.GetAllAsync(aParams.PageIndex,aParams.PageSize,aParams.Search);
        var lista = _mapper.Map<List<RolDto>>(registros);
        return new Pager<RolDto>(lista,totalRegistros,aParams.PageIndex,aParams.PageSize,aParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(RolPDto rolDto)
    {
        var rol = _mapper.Map<Rol>(rolDto);
        _unitOfWork.Roles.Add(rol);
        await _unitOfWork.SaveAsync();
        if (rol == null)
        {
            return BadRequest();
        }
        return "Rol Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] RolPDto rolDto)
    {
        if (rolDto == null|| id != rolDto.Id)
        {
            return BadRequest();
        }
        var rol = await _unitOfWork.Roles.GetByIdAsync(id);

        if (rol == null)
        {
            return NotFound();
        }
        _mapper.Map(rolDto, rol);
        _unitOfWork.Roles.Update(rol);
        await _unitOfWork.SaveAsync();

        return "Rol Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var rol = await _unitOfWork.Roles.GetByIdAsync(id);
        if (rol == null)
        {
            return NotFound();
        }
        _unitOfWork.Roles.Remove(rol);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Rol {rol.Descripcion} se eliminó con éxito." });
    }
}