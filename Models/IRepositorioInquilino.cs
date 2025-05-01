using StiegerInmobiliaria.DTOs;
namespace StiegerInmobiliaria.Models
{

    interface IrepositorioInquilino : iRepositorio<InquilinoModel>
    {
        public InquilinoDTO traerIdDTO(int id);
        public List<InquilinoModel> Busqueda(string dato);
    }
    
}