using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Models
{
    public interface iRepositorioContrato : iRepositorio<ContratoModel>{

        public List<ContratoJSON> Busqueda(string dato);

        public int Cancelar(ContratoModel c);

        public List<ContratoDTO> TraerTodosDTO(int paginaNro, int tamPagina);

        public List<ContratoDTO> BuscarVigenteXFechas(int paginaNro, int tamPagina,string inicio ,string fin);

    }
}