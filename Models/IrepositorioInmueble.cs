using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Models
{

    public interface IrepositorioInmueble : iRepositorio<InmuebleModel>
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

        public List<InmuebleDTO> InmueblesxPropietario(int id);

        public List<InmuebleModel> ListarXDisponible(string estado, int paginaNro, int tamPagina);

        public int TraerCantidadxEstado(string estado);

        public List<InmuebleModel> ListarDesocupadoXFechas(string inicio, string fin,int paginaNro, int tamPagina);
    }



}