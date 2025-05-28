using Microsoft.AspNetCore.Components.Forms;
using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Models
{
    public interface IrepositorioPagos : iRepositorio<PagoModel>
    {

        public PagoDTO traerIdDTO(int id);

         public List<PagoDTO> TraerTodosDTO(int paginaNro, int tamPagina);

         public List<PagoModel> IdPagosXFechaFin(string fecha,int idContrato);

         public int BajaUser(int id,int usr);

         public List<PagoModel> PagosXContrato(int idContrato);

    }
}