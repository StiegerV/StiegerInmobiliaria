using System.ComponentModel.DataAnnotations;
namespace StiegerInmobiliaria.Models
{

    public class UsuarioModel
    {
        [Key]
        public int Id_usuario { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        //[StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [DataType(DataType.Password)]
        public string? Contraseña { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string? Rol { get; set; }

        public string? Imagen { get; set; }
        public IFormFile? ImgArchivo { get; set; }


    }
}