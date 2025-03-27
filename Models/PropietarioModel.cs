using System.ComponentModel.DataAnnotations;
using Name;

namespace StiegerModels
{
    class PropietarioModel : personaModel
    {
        [Key]
        public int Id_propietario { get; set; }

        [Required]
        public string telefono { get; set; }
        
        public PropietarioModel(){}
        public PropietarioModel(string dni,string nombre,string apellido,string telefono){
            this.Dni=dni;
            this.Nombre=nombre;
            this.Apellido=apellido;
            this.telefono=telefono;
        }
    }
}