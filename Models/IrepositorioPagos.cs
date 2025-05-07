using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Models
{
    public interface IrepositorioPagos : iRepositorio<PagoModel>
    {

        public PagoDTO traerIdDTO(int id);

         public List<PagoDTO> TraerTodosDTO();

    }
}