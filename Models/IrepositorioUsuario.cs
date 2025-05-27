using StiegerInmobiliaria.DTOs;
namespace StiegerInmobiliaria.Models
{
    public interface IrepositorioUsuario : iRepositorio<UsuarioModel>
    {
        public UsuarioModel obtenerXNombre(string usuario);

        public List<UsuarioDTO> TraerTodosDTO();
    }
}