using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Models
{

    interface IrepositorioInmueble : iRepositorio<InmuebleModel>
    {
        public int ContratoActivo(int id);

        public int EliminarImagen(InmuebleModel inmueble);


        /// <summary>
        /// permite conseguir el dto del propietario a traves de su id del inmueble asociado al inmueble
        /// </summary>
        /// <param name="id">el id del propietario asociado al inmueble.</param>
        public PropietarioDTO TraerIdPropietarioDTO(int id);

        public InmuebleDTO TraerIdDTO(int id);

        public List<InmuebleDTO> traerDesocupados(string inicio, string fin);
    }

    

}