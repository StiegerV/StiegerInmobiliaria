using StiegerInmobiliaria.DTOs;
namespace StiegerInmobiliaria.Models
{

   public interface IrepositorioInquilino : iRepositorio<InquilinoModel>
    {
        public InquilinoDTO traerIdDTO(int id);
        public List<InquilinoModel> Busqueda(string dato);
    }
    
}