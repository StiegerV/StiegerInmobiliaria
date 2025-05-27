namespace StiegerInmobiliaria.Models
{
    public class PagoModel
    {
        public int Id_pago { get; set; }

        public int Id_contrato { get; set; }

        public double Monto { get; set; }

        public DateTime Fecha { get; set; }


        public string? Observacion { get; set; }

        public string? Estado { get; set; }

        public int Creado_por { get; set; }

        public int Finalizado_por { get; set; }



        public override string ToString()
        {
            return $"Pago ID: {Id_pago}, Contrato ID: {Id_contrato}, Monto: {Monto:C}, Fecha: {Fecha:yyyy-MM-dd}, Estado: {Estado}, Observación: {Observacion}";
        }


    }
}