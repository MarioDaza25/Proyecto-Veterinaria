using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class EspecieController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EspecieController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<EspecieDto>>> Get([FromQuery] Params especieParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Especies.GetAllAsync(especieParams.PageIndex,especieParams.PageSize,especieParams.Search);
        var listaEspecies = _mapper.Map<List<EspecieDto>>(registros);
        return new Pager<EspecieDto>(listaEspecies,totalRegistros,especieParams.PageIndex,especieParams.PageSize,especieParams.Search);
    }


    //Mascotas que se encuentren registradas cuya especie sea felina.
    [HttpGet("ConMascotas/{especie}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EspecieXRazaDto>>> MascotaXUnaEspecie(string especie)
    {
        if (especie == "")
        {
            return BadRequest("Ingrese una Especie.");
        }
        var mascotas = await _unitOfWork.Especies.MascotaXUnaEspecie(especie);
        return _mapper.Map<List<EspecieXRazaDto>>(mascotas);
    } 

    //Listar todas las mascotas agrupadas por especie..
    [HttpGet("ConMascotas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EspecieXRazaDto>>> MascotaXEspecie()
    {
        var mascotas = await _unitOfWork.Especies.MascotaXEspecie();
        return _mapper.Map<List<EspecieXRazaDto>>(mascotas);
    } 


    [HttpDelete("{nombreEspecie}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(string nombreEspecie)
    {
        var especie = await _unitOfWork.Especies.GetByIdAsync(nombreEspecie);
        if (especie == null)
        {
            return NotFound();
        }
        _unitOfWork.Especies.Remove(especie);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
