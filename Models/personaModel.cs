using System.ComponentModel.DataAnnotations;

namespace StiegerInmobiliaria.Models
{
   public class personaModel 
    {
        [Required]
        public string Dni { get; set; }= string.Empty;

        [Required]
        public string Nombre { get; set; }= string.Empty;


        [Required]
        public string Apellido { get; set; }= string.Empty;

        
        public string Telefono { get; set; }= string.Empty;

        public string Mail { get; set; }= string.Empty;
    }
}
