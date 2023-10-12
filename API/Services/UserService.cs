using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.Dtos;
using API.Helpers;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class UserService : IUserService
{
     private readonly JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    public UserService()
    {
    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
         var usuario = new Usuario
        {
            Email = registerDto.Email,
            Username = registerDto.Username,
        };
        usuario.Password = _passwordHasher.HashPassword(usuario, registerDto.Password); //Encrypt password

        var existingUser = _unitOfWork.Usuarios
                                    .Find(u => u.Username.ToLower() == registerDto.Username.ToLower())
                                    .FirstOrDefault();
        if (existingUser == null)
        {
            var rolDefault = _unitOfWork.Roles
                                    .Find(u => u.Descripcion == Autorizacion.rol_predeterminado.ToString())
                                    .First();
            try
            {
                usuario.Roles.Add(rolDefault);
                _unitOfWork.Usuarios.Add(usuario);
                await _unitOfWork.SaveAsync();

                return $"{registerDto.Username} ha sido registrado exitosamente";
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return $"Error: {message}";
            }
        }
        else
        {
            return $"El usuario {registerDto.Username} ya existe!";
        }
    }

    //Agregar Roles al Usuario
    public async Task<string> AddRoleAsync(AddRoleDto addRol)
    {
        var usuario = await _unitOfWork.Usuarios
                            .GetByUsernameAsync(addRol.Username);

        if (usuario == null)
        {
            return $"{addRol.Username} No existe!!.";
        }

        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, addRol.Password);

        if (resultado == PasswordVerificationResult.Success)
        {
            var rolExiste = _unitOfWork.Roles
                                            .Find(u => u.Descripcion.ToLower() == addRol.Rol.ToLower())
                                            .FirstOrDefault();

            if (rolExiste != null)
            {
                var usuarioTieneRol = usuario.Roles
                                                .Any(u => u.Id == rolExiste.Id);

                if (usuarioTieneRol == false)
                {
                    usuario.Roles.Add(rolExiste);
                    _unitOfWork.Usuarios.Update(usuario);
                    await _unitOfWork.SaveAsync();
                }

                return $"Rol {addRol.Rol} agregado a la cuenta {addRol.Username} de forma exitosa.";
            }

            return $"Rol {addRol.Rol} no encontrado.";
        }

        return $"Credenciales incorrectas para el ususario {usuario.Username}.";
    }



    public async Task<DataUserDto> GetTokenAsync(LoginDto login)
    {
        DataUserDto datosUsuarioDto = new();
        var usuario = await _unitOfWork.Usuarios.GetByUsernameAsync(login.Username);

        if (usuario == null)
        {
            datosUsuarioDto.EstaAutenticado = false;
            datosUsuarioDto.Mensaje = $"{login.Username} No existe!!.";
            return datosUsuarioDto;
        }

        var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, login.Password);
        if (result == PasswordVerificationResult.Success)
        {
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);

            datosUsuarioDto.Mensaje = "OK";
            datosUsuarioDto.EstaAutenticado = true;
            datosUsuarioDto.Username = usuario.Username;
            datosUsuarioDto.Email = usuario.Email;
            datosUsuarioDto.Roles = usuario.Roles
                                                .Select(p => p.Descripcion)
                                                .ToList();
            datosUsuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            //datosUsuarioDto.RefreshToken = CreateRefreshToken();
            //datosUsuarioDto.ExpiracionRefreshToken =

            return datosUsuarioDto;
        }    
        else
        {
            datosUsuarioDto.EstaAutenticado = false;
            datosUsuarioDto.Mensaje = $"Credenciales incorrectas para el usuario {usuario.Username}.";   
        }
        return datosUsuarioDto;
    }



    private JwtSecurityToken CreateJwtToken(Usuario usuario)
    {
        if (usuario == null)
        {
            throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo.");
        }

        var roles = usuario.Roles;
        var rolClaims = new List<Claim>();
        foreach (var rol in roles)
        {
            rolClaims.Add(new Claim("roles", rol.Descripcion));
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("uid", usuario.Id.ToString())
        }
        .Union(rolClaims);

        if (string.IsNullOrEmpty(_jwt.Key) || string.IsNullOrEmpty(_jwt.Issuer) || string.IsNullOrEmpty(_jwt.Audience))
        {
            throw new ArgumentNullException("La configuración del JWT es nula o vacía.");
        }

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        var JwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: signingCredentials);

        return JwtSecurityToken;
    }

    public async Task<DataUserDto> RefreshTokenAsync(string refreshToken)
    {
        var dataUserDto = new DataUserDto();

        var usuario = await _unitOfWork.Usuarios.GetByRefreshTokenAsync(refreshToken);

        if (usuario == null)
        {
            dataUserDto.EstaAutenticado = false;
            dataUserDto.Mensaje = $"El token no está asignado a ningún usuario..";
            return dataUserDto;
        }

        var refreshTokenBd = usuario.RefreshTokens.Single(x => x.Token == refreshToken);

        if (!refreshTokenBd.IsActive)
        {
            dataUserDto.EstaAutenticado = false;
            dataUserDto.Mensaje = $"El Token no esta activo.";
            return dataUserDto;
        }
        
        //Revoque the current refresh token and
        refreshTokenBd.Revoked = DateTime.UtcNow;

        //generate a new refresh token and save it in the database
        var newRefreshToken = CreateRefreshToken();
        usuario.RefreshTokens.Add(newRefreshToken);
        _unitOfWork.Usuarios.Update(usuario);
        await _unitOfWork.SaveAsync();

        //Generate a new Json Web Token
        dataUserDto.EstaAutenticado = true;
        JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
        dataUserDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        dataUserDto.Email = usuario.Email;
        dataUserDto.Username = usuario.Username;
        dataUserDto.Roles = usuario.Roles
                                        .Select(u => u.Descripcion)
                                        .ToList();
        dataUserDto.RefreshToken = newRefreshToken.Token;
        dataUserDto.RefreshTokenExpiration = newRefreshToken.Expiracion;
        return dataUserDto;
    }
    private RefreshToken CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expiracion = DateTime.UtcNow.AddMinutes(3),
                Creacion = DateTime.UtcNow
            };
        }
    }

    
}
