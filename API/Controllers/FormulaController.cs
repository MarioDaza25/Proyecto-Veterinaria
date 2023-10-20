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
public class FormulaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public FormulaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<FormulaDto>>> Get()
    {
        var formulas = await _unitOfWork.FormulasMedicas.GetAllAsync();
        return _mapper.Map<List<FormulaDto>>(formulas);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<FormulaDto>>> Get([FromQuery] Params formulaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.FormulasMedicas.GetAllAsync(formulaParams.PageIndex,formulaParams.PageSize,formulaParams.Search);
        var listaFormulas = _mapper.Map<List<FormulaDto>>(registros);
        return new Pager<FormulaDto>(listaFormulas,totalRegistros,formulaParams.PageIndex,formulaParams.PageSize,formulaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(FormulaMedicaPDto formulaDto)
    {
        var formula = _mapper.Map<FormulaMedica>(formulaDto);
        _unitOfWork.FormulasMedicas.Add(formula);
        await _unitOfWork.SaveAsync();
        if (formula == null)
        {
            return BadRequest();
        }
        return "Formula Medica Creada con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] FormulaMedicaPDto formulaDto)
    {
        if (formulaDto == null|| id != formulaDto.Id)
        {
            return BadRequest();
        }
        var formula = await _unitOfWork.FormulasMedicas.GetByIdAsync(id);

        if (formula == null)
        {
            return NotFound();
        }
        _mapper.Map(formulaDto, formula);
        _unitOfWork.FormulasMedicas.Update(formula);
        await _unitOfWork.SaveAsync();

        return "Formula Medica Actualizada con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var formula = await _unitOfWork.FormulasMedicas.GetByIdAsync(id);
        if (formula == null)
        {
            return NotFound();
        }
        _unitOfWork.FormulasMedicas.Remove(formula);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"La formula {formula.Id} se eliminó con éxito." });
    }
}
