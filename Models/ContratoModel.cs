using System.ComponentModel.DataAnnotations;

namespace StiegerInmobiliaria.Models
{
    public class ContratoModel
    {
        [Required(ErrorMessage = "El ID del contrato es obligatorio.")]
        public int Id_contrato { get; set; }

        [Required(ErrorMessage = "El ID del inmueble es obligatorio.")]
        public int Id_inmueble { get; set; }

        [Required(ErrorMessage = "El ID del inquilino es obligatorio.")]
        public int Id_inquilino { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Fecha_fin_original { get; set; }

        [Required(ErrorMessage = "El usuario creador es obligatorio.")]
        public int Creador_por { get; set; }

        public int? Terminado_por { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 0.")]
        public float Monto { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }


        public override string ToString()
        {
            return $"ID Contrato #{Id_contrato}: " +
                    $"ID Inmueble: {Id_inmueble}, " +
                   $"ID Inquilino: {Id_inquilino}, " +
                   $"creado por{Creador_por}" +
                   $"terminado por{Terminado_por}" +
                   $"monto {Monto}" +
                   $"fecha inicio {FechaInicio}" +
                   $"fecha fin {FechaFin}";
        }
    }
}