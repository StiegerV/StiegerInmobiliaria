using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
namespace StiegerInmobiliaria.Models
{
    public class InmuebleModel
    {
        [Required]
        public int Id_inmueble { get; set; }

        [Required]
        public int Id_propietario { get; set; }

        public string? Direccion { get; set; } = "";

        //COMERCIAL RESIDENCIAL
        public string? Uso { get; set; } = "";

        //LOCAL CASA DEPARTAMENTO
        public string? Tipo { get; set; } = "";

        public int? Ambientes { get; set; }

        public string? Cordenadas { get; set; } = "";

        public double? Precio { get; set; }

        //disponible alquilado suspendido
        public string? Disponible { get; set; } = "";

        public string? Imagen { get; set; } = "";

        [NotMapped,AllowNull]
        public IFormFile ImagenArchivo { get; set; }

        public override string ToString()
        {
            return $"Propietario{Id_propietario} Inmueble #{Id_inmueble} - Dirección: {Direccion}, " +
                   $"Tipo: {Tipo}, Uso: {Uso}, Ambientes: {Ambientes}, " +
                   $"Precio: ${Precio}, Estado: {Disponible},Imagen: {Imagen}";
        }


    }


}