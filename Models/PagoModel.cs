namespace StiegerInmobiliaria.Models
{
    public class PagoModel
    {
        public int Id_pago {get; set;}

        public int Id_contrato{get;set;}

        public double Monto{get;set;}

        public DateTime fecha{get;set;}


        public string? Observacion{get;set;}

        public string? Estado{get;set;}

    }
}