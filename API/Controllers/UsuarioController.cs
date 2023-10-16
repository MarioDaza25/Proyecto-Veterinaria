using API.Dtos;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    
public class UsuarioController : BaseApiController
{
    private readonly IUserService _userService;
    public UsuarioController(IUserService userService)
    {
        _userService = userService;
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
}
