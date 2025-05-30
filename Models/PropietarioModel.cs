using System.ComponentModel.DataAnnotations;

namespace StiegerInmobiliaria.Models
{
    public class PropietarioModel : personaModel
    {
        [Key]
        public int Id_propietario { get; set; }

        public override string ToString()
        {
            return $"{Nombre} {Apellido}, dni: {Dni}";
        }
    }
}