namespace StiegerInmobiliaria.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PagoModel
    {
        [Key]
        public int Id_pago { get; set; }

        [Required(ErrorMessage = "El ID del contrato es obligatorio.")]
        public int Id_contrato { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public double Monto { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [StringLength(250, ErrorMessage = "La observación no puede tener más de 250 caracteres.")]
        public string? Observacion { get; set; }

        [StringLength(50, ErrorMessage = "El estado no puede tener más de 50 caracteres.")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "El campo 'Creado por' es obligatorio.")]
        public int Creado_por { get; set; }

        public int Finalizado_por { get; set; }

        public override string ToString()
        {
            return $"Pago ID: {Id_pago}, Contrato ID: {Id_contrato}, Monto: {Monto:C}, Fecha: {Fecha:yyyy-MM-dd}, Estado: {Estado}, Observación: {Observacion}";
        }
    }

}