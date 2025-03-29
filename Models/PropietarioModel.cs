using System.ComponentModel.DataAnnotations;
using Name;

namespace StiegerModels
{
    public class PropietarioModel : personaModel
    {
        [Key]
        public int Id_propietario { get; set; }

        [Required]
        public string telefono { get; set; }

        public PropietarioModel() { }
        public PropietarioModel(int id, string dni, string nombre, string apellido, string telefono)
        {
            this.Id_propietario = id;
            this.Dni = dni;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.telefono = telefono;
        }
        public PropietarioModel(string dni, string nombre, string apellido, string telefono)
        {
            this.Dni = dni;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.telefono = telefono;
        }

        public override string ToString()
        {
            return $"{Nombre} {Apellido}, dni: {Dni}";
        }
    }
}