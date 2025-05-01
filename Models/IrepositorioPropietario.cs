using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Models
{

public interface IrepositorioPropietario :iRepositorio<PropietarioModel>
    {
          public List<PropietarioModel> Busqueda(string dato);

          public PropietarioDTO traerIdDTO(int id);
    }
}