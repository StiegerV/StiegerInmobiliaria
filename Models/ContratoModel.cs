using System.ComponentModel.DataAnnotations;

namespace StiegerInmobiliaria.Models
{
    public class ContratoModel
    {
        [Required]
        public int Id_contrato { get; set; }

        [Required]
        public int Id_inmueble { get; set; }

        [Required]
        public int Id_inquilino { get; set; }

        [Required]
        public int Creador_por { get; set; }

        public int? Terminado_por { get; set; }
        [Required]
        public float Monto { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
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