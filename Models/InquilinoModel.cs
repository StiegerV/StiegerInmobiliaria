using System.ComponentModel.DataAnnotations;

namespace StiegerInmobiliaria.Models
{
    public class InquilinoModel : personaModel
    {
        [Key]
        public int Id_inquilino { get; set; }

        public InquilinoModel() { }

        public InquilinoModel(string dni,string nombre,string apellido,string telefono,string mail){
            this.Dni = dni;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Telefono = telefono;
            this.Mail = mail;
        }

        public InquilinoModel(string dni, string nombre, string apellido, string mail){
            this.Dni = dni;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Mail = mail;
        }

        public InquilinoModel(string dni, string nombre, string apellido){
            this.Dni = dni;
            this.Nombre = nombre;
            this.Apellido = apellido;
        }


        public override string ToString()
        {
            return $"nombre:{Nombre} apellido:{Apellido}, dni: {Dni}";
        }
    }
}
