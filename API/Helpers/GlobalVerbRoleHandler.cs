using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace API.Helpers;

public class GlobalVerbRoleHandler : AuthorizationHandler<GlobalVerbRoleRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GlobalVerbRoleHandler(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GlobalVerbRoleRequirement requirement)
    {
        // comprobar si el usuario tiene los roles requeridos para la accion actual
        var roles = context.User.FindAll(c => string.Equals(c.Type, ClaimTypes.Role)).Select(c => c.Value);
        var verb = _httpContextAccessor.HttpContext?.Request.Method;
        if (string.IsNullOrEmpty(verb)) { throw new Exception($"¡La solicitud no puede ser nula!"); }
        foreach (var rol in roles)
        {
            if (requirement.IsAllowed(rol, verb))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }
        context.Fail();
        return Task.CompletedTask;
    }
}
