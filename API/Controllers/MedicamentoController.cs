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
    public async Task<ActionResult<IEnumerable<MedicamentoDto>>> Get()
    {
        var medicamentos = await _unitOfWork.Medicamentos.GetAllAsync();
        return _mapper.Map<List<MedicamentoDto>>(medicamentos);
    }


    [HttpGet]
    [ApiVersion("1.1")]
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

    //Listar los proveedores que me venden un determinado medicamento.
    [HttpGet("IncluyeProveedor/{medicamento}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicamentoConProvDto>>> MedicamentoConProveedor(string medicamento)
    {
        var medicamentos = await _unitOfWork.Medicamentos.MedicamentoConProveedor(medicamento);
        return _mapper.Map<List<MedicamentoConProvDto>>(medicamentos);
    } 

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(MedicamentoPDto medicamentoDto)
    {
        //var medicamento = _mapper.Map<Medicamento>(medicamentoDto);
        
        MedicamentoProveedor  medicamentoProveedor = new () 
        {
            Id_Medicamento = medicamentoDto.Id,
            Id_Proveedor = medicamentoDto.Id_Proveedor
        }; 

        Medicamento medicamento = new () 
        {
            Nombre = medicamentoDto.Nombre,
            Cantidad = medicamentoDto.Cantidad,
            Precio = medicamentoDto.Precio,
            Id_Laboratorio = medicamentoDto.Id_Laboratorio

        };

        _unitOfWork.Medicamentos.Add(medicamento);
        await _unitOfWork.SaveAsync();
        if (medicamento == null)
        {
            return BadRequest();
        }
        return "Medicamento Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] MedicamentoPDto medicamentoDto)
    {
        if (medicamentoDto == null|| id != medicamentoDto.Id)
        {
            return BadRequest();
        }
        var medicamento = await _unitOfWork.Medicamentos.GetByIdAsync(id);

        if (medicamento == null)
        {
            return NotFound();
        }
        _mapper.Map(medicamentoDto, medicamento);
        _unitOfWork.Medicamentos.Update(medicamento);
        await _unitOfWork.SaveAsync();

        return "Medicamento Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var medicamento = await _unitOfWork.Medicamentos.GetByIdAsync(id);
        if (medicamento == null)
        {
            return NotFound();
        }
        _unitOfWork.Medicamentos.Remove(medicamento);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Medicamento {medicamento.Nombre} se eliminó con éxito." });
    }
}