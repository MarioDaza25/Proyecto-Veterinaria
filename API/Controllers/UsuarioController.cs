using API.Dtos;
using API.Helpers;
using API.Services;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    
public class UsuarioController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public UsuarioController(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost("Registro")]
    public async Task<IActionResult> RegisterAsync(RegisterDto model)
    {
        var result = await _userService.RegisterAsync(model);
        return Ok(result);
    }

    [HttpPost("Token")]
    public async Task<IActionResult> GetTokenAsync(LoginDto model)
    {
        var result = await _userService.GetTokenAsync(model);
        
        return Ok(result);
    }

    [HttpPost("AgregarRol")]
    public async Task<IActionResult> AddRoleAsync(AddRoleDto model)
    {
        var result = await _userService.AddRoleAsync(model);
        return Ok(result);
    }
    
    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = await _userService.RefreshTokenAsync(refreshToken);
        if (!string.IsNullOrEmpty(response.RefreshToken))
            SetRefreshTokenInCookie(response.RefreshToken);
        return Ok(response);
    }

    
    private void SetRefreshTokenInCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(10),
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<UsuarioDto>>> Get([FromQuery] Params aParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Usuarios.GetAllAsync(aParams.PageIndex,aParams.PageSize,aParams.Search);
        var lista = _mapper.Map<List<UsuarioDto>>(registros);
        return new Pager<UsuarioDto>(lista,totalRegistros,aParams.PageIndex,aParams.PageSize,aParams.Search);
    }
}
