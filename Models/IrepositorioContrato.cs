using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Models
{
    public interface iRepositorioContrato : iRepositorio<ContratoModel>{

        public List<ContratoJSON> Busqueda(string dato);

    }
}