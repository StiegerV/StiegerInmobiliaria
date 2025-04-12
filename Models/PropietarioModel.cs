using System.ComponentModel.DataAnnotations;

namespace StiegerModels
{
    public class PropietarioModel : personaModel
    {
        [Key]
        public int Id_propietario { get; set; }

        public PropietarioModel() { }
        public PropietarioModel(int id, string dni, string nombre, string apellido, string telefono,string mail)
        {
            this.Id_propietario = id;
            this.Dni = dni;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Telefono = telefono;
            this.Mail=mail;
        }
        public PropietarioModel(string dni, string nombre, string apellido, string telefono)
        {
            this.Dni = dni;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Telefono = telefono;
        }


        public override string ToString()
        {
            return $"{Nombre} {Apellido}, dni: {Dni}";
        }
    }
}