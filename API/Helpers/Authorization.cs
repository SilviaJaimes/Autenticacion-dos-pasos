namespace API.Helpers;

public class Authorization
{
    public enum Roles
    {
        Administrator,
        Empleado,
        Proveedor,
        Paciente,
        Doctor,
        Vendedor
    }

    public const Roles rol_default = Roles.Empleado;
}
