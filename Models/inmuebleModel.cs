using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
namespace StiegerInmobiliaria.Models
{
    public class InmuebleModel
    {
        [Key]
        [Required(ErrorMessage = "El ID del inmueble es obligatorio.")]
        public int Id_inmueble { get; set; }

        [Required(ErrorMessage = "El propietario es obligatorio.")]
        public int Id_propietario { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(100, ErrorMessage = "La dirección no puede tener más de 100 caracteres.")]
        public string? Direccion { get; set; } = "";

        [Required(ErrorMessage = "El uso del inmueble es obligatorio.")]
        [StringLength(30, ErrorMessage = "El uso no puede tener más de 30 caracteres.")]
        public string? Uso { get; set; } = ""; // Comercial, Residencial

        [Required(ErrorMessage = "El tipo de inmueble es obligatorio.")]
        [StringLength(30, ErrorMessage = "El tipo no puede tener más de 30 caracteres.")]
        public string? Tipo { get; set; } = ""; // Casa, Departamento, Local

        [Range(1, 50, ErrorMessage = "La cantidad de ambientes debe estar entre 1 y 50.")]
        public int? Ambientes { get; set; }

        [StringLength(100, ErrorMessage = "Las coordenadas no pueden tener más de 100 caracteres.")]
        public string? Cordenadas { get; set; } = "";

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public double? Precio { get; set; }

        [Required(ErrorMessage = "El estado de disponibilidad es obligatorio.")]
        [StringLength(20, ErrorMessage = "El estado de disponibilidad no puede tener más de 20 caracteres.")]
        public string? Disponible { get; set; } = ""; // Disponible, Alquilado, Suspendido

        [StringLength(255, ErrorMessage = "La ruta de la imagen no puede tener más de 255 caracteres.")]
        public string? Imagen { get; set; } = "";

        [NotMapped]
        public IFormFile? ImagenArchivo { get; set; }

        public override string ToString()
        {
            return $"Propietario{Id_propietario} Inmueble #{Id_inmueble} - Dirección: {Direccion}, " +
                   $"Tipo: {Tipo}, Uso: {Uso}, Ambientes: {Ambientes}, " +
                   $"Precio: ${Precio}, Estado: {Disponible},Imagen: {Imagen}";
        }


    }


}