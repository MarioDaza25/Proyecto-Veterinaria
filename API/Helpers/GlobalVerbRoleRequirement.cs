using Microsoft.AspNetCore.Authorization;

namespace API.Helpers;

public class GlobalVerbRoleRequirement : IAuthorizationRequirement
{
    public bool IsAllowed(string rol, string verb)
    {
        // Permitir todas las acciones si el usuario es "admin"
        if(string.Equals("Administrador", rol, StringComparison.OrdinalIgnoreCase)) return true;
        // Permitir todas las acciones si el usuario es "Gerente"
        if(string.Equals("Gerente", rol, StringComparison.OrdinalIgnoreCase)) return true;
        // Permitir todas las acciones si el usuario es "Empleado"
        if(string.Equals("empleado", rol, StringComparison.OrdinalIgnoreCase) && string.Equals("GET",verb, StringComparison.OrdinalIgnoreCase)){
            return true;
        };
        // ... Agregar otros Roles
        return false;
    }        
    
}