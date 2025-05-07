using System.ComponentModel.DataAnnotations;

namespace StiegerInmobiliaria.DTOs
{
    public class ContratoDTO
    {
        public int Id_contrato { get; set; }

        public InquilinoDTO? Inquilino { get; set; }

        public PropietarioDTO? Propietario { get; set; }

        public InmuebleDTO? Inmueble { get; set; }

        public DateTime Fecha_inicio { get; set; }

        public DateTime Fecha_fin { get; set; }

        public UsuarioDTO? CreadoPor { get; set; }

        public UsuarioDTO? FinalizadoPor { get; set; }

        public override string ToString()
        {
            return $"Contrato #{Id_contrato}: " +
                   $"Inquilino: {Inquilino?.Id_inquilino} {Inquilino?.Nombre} {Inquilino?.Apellido}, " +
                   $"Propietario: {Propietario?.Id_propietario} {Propietario?.Nombre} {Propietario?.Apellido}, " +
                   $"Inmueble: {Inmueble?.Direccion}, " +
                   $"Desde {Fecha_inicio:dd/MM/yyyy} hasta {Fecha_fin:dd/MM/yyyy}";
        }


    }
}