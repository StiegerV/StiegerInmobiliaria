using System.Security.Cryptography.X509Certificates;

namespace StiegerInmobiliaria.DTOs
{

    //`id_contrato`,c.`id_inmueble`,m.tipo,c.id_inquilino,i.apellido
    public class ContratoJSON
    {
        public int Id_contrato { get; set; }

        public int Id_inmueble { get; set; }
        public string? Tipo_inmueble { get; set; }

        public int Id_inquilino { get; set; }

        public string? Apellido_inquilino { get; set; }

    }
}
