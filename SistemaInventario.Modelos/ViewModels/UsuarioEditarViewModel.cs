// UsuarioEditarViewModel.cs
using System.ComponentModel.DataAnnotations;

public class UsuarioEditarViewModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string NumeroDocumento { get; set; }

    [Required]
    [MaxLength(60)]
    public string Nombre { get; set; }

    [Required]
    [MaxLength(60)]
    public string Apellido { get; set; }

    [Required]
    [EmailAddress]
    public string Correo { get; set; }

    // En la edición, contraseña es OPCIONAL
    public string? ContraseñaHash { get; set; }

    [Required]
    public string Rol { get; set; }

    [Required]
    [MaxLength(40)]
    public string Ciudad { get; set; }

    [Required]
    [MaxLength(25)]
    public string NombreUsuario { get; set; }
}
