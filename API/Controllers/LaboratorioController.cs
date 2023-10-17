using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class LaboratorioController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public LaboratorioController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<LaboratorioDto>>> Get([FromQuery] Params labParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Laboratorios.GetAllAsync(labParams.PageIndex,labParams.PageSize,labParams.Search);
        var listaLab = _mapper.Map<List<LaboratorioDto>>(registros);
        return new Pager<LaboratorioDto>(listaLab,totalRegistros,labParams.PageIndex,labParams.PageSize,labParams.Search);
    }


    //Medicamentos que pertenezcan a el laboratorio Genfar
    [HttpGet("ConMedicamentos/{laboratorio}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<LabxMedicaDto>>> MedicamentoXLaboratorio(string laboratorio)
    {
        if (laboratorio == "")
        {
            return BadRequest("Ingrese una Especialidad.");
        }
        var medicamento = await _unitOfWork.Laboratorios.MedicamentoXLaboratorio(laboratorio);
        return _mapper.Map<List<LabxMedicaDto>>(medicamento);
    } 
}
