using System.ComponentModel.DataAnnotations;

namespace Name
{
    class personaModel
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
