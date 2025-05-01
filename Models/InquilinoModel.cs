using System.ComponentModel.DataAnnotations;

namespace StiegerInmobiliaria.Models
{
    public class InquilinoModel : personaModel
    {
        [Key]
        public int Id_inquilino { get; set; }

        public InquilinoModel() { }

       


        public override string ToString()
        {
            return $"nombre:{Nombre} apellido:{Apellido}, dni: {Dni}";
        }
    }
}
