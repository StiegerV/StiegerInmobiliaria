using System.ComponentModel.DataAnnotations;
namespace StiegerInmobiliaria.Models
{
    public class InmuebleModel
    {
        [Required]
        public int Id_inmueble { get; set; }

        [Required]
        public int Id_propietario { get; set; }

        public string Direccion { get; set; }

        //COMERCIAL RESIDENCIAL
        public string Uso { get; set; }

        //LOCAL CASA DEPARTAMENTO
        public string Tipo { get; set; }

        public int Ambientes { get; set; }

        public string Cordenadas { get; set; }

        public double Precio { get; set; }

        //disponible alquilado suspendido
        public string Disponible { get; set; }

        public string Imagen { get; set; }
    }
}