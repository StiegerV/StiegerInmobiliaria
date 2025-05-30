using System.ComponentModel.DataAnnotations;

namespace StiegerInmobiliaria.Models
{
    public class personaModel
    {
        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [StringLength(20, ErrorMessage = "El DNI no puede tener más de 20 caracteres.")]
        public string Dni { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        public string Apellido { get; set; } = string.Empty;

        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        [StringLength(20, ErrorMessage = "El teléfono no puede tener más de 20 caracteres.")]
        public string? Telefono { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [StringLength(100, ErrorMessage = "El correo no puede tener más de 100 caracteres.")]
        public string? Mail { get; set; } = string.Empty;
    }
}
