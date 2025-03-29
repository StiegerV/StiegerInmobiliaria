using System.ComponentModel.DataAnnotations;
using Name;

namespace StiegerModels
{
   public class InquilinoModel : personaModel
    {
        [Key]
        public int Id_inquilino { get; set; }


        public InquilinoModel() { }

        public InquilinoModel(string dni, string nombre, string apellido, string contacto)
        {
            this.Dni = dni;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Contacto = contacto;
        }


        public override string ToString()
        {
            return $"{Nombre} {Apellido}, dni: {Dni}";
        }
    }



}