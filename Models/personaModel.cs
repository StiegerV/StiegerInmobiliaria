using System.ComponentModel.DataAnnotations;

namespace Name
{
   public class personaModel
    {
        [Required]
        public string Dni { get; set; }

        [Required]
        public string Nombre { get; set; }


        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Contacto { get; set; }
    }
}
