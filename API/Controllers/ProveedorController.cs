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
public class ProveedorController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProveedorController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> Get()
    {
        var proveedores = await _unitOfWork.Proveedores.GetAllAsync();
        return _mapper.Map<List<ProveedorDto>>(proveedores);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProveedorDto>>> Get([FromQuery] Params ProveedParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Proveedores.GetAllAsync(ProveedParams.PageIndex,ProveedParams.PageSize,ProveedParams.Search);
        var listaProv = _mapper.Map<List<ProveedorDto>>(registros);
        return new Pager<ProveedorDto>(listaProv,totalRegistros,ProveedParams.PageIndex,ProveedParams.PageSize,ProveedParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(ProveedorPDto proveedorDto)
    {
        var proveedor = _mapper.Map<Proveedor>(proveedorDto);
        _unitOfWork.Proveedores.Add(proveedor);
        await _unitOfWork.SaveAsync();
        if (proveedor == null)
        {
            return BadRequest();
        }
        return "Proveedor Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] ProveedorPDto proveedorDto)
    {
        if (proveedorDto == null|| id != proveedorDto.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Proveedores.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(proveedorDto, proveedExiste);
        _unitOfWork.Proveedores.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Proveedor Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id);
        if (proveedor == null)
        {
            return NotFound();
        }
        _unitOfWork.Proveedores.Remove(proveedor);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Proveedor {proveedor.Nombre} se eliminó con éxito." });
    }
}