using StiegerInmobiliaria.Models;

namespace StiegerInmobiliaria.DTOs
{
    public class PagoDTO
    {
        public int Id_pago { get; set; }

        public int Id_contrato { get; set; }
        public double Monto { get; set; }

        public DateTime Fecha { get; set; }

        public string? Observacion { get; set; }

        public string? Estado { get; set; }

        public int Id_inquilino { get; set; }

        public string? Apellido { get; set; }

        public int Id_inmueble { get; set; }

        public string? Tipo { get; set; }

        public override string ToString()
        {
            return $"{Tipo}"+" "+$"{Apellido}";
        }

        public string Test(){
            return $"idp:{Id_pago} idc:{Id_contrato} m:{Monto} f:{Fecha} o:{Observacion} e:{Estado} idI:{Id_inquilino} ap_{Apellido} idI:{Id_inmueble} ti:{Tipo}";
        }

    }
}