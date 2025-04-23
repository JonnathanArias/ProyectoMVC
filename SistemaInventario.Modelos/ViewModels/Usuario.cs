using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaInventario.Modelos;


public class Usuario
{

    [Key]  // Indica que es la clave primaria
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Hace que sea autoincremental
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

    [Required]
    public string ContraseñaHash { get; set; }  // La contraseña se almacena encriptada
    [Required]
    public string Rol { get; set; }

    [Required]
    [MaxLength(40)]
    public string Ciudad { get; set; }

    [Required]
    [MaxLength(25)]
    public string NombreUsuario { get; set; }

    [Required]

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    //este true es cuando agregue el producto indique si hay pero si hay modificacion pasara a 0
    public bool Estado { get; set; } = true;

    public bool DebeCambiarContraseña { get; set; } = true;  // Para obligar a cambiar la contraseña

}