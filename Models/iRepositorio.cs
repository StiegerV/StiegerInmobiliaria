namespace StiegerInmobiliaria.Models
{
    // <T> indica que es una interfaz generica que puede recibir varios tipos de objetos

   public interface iRepositorio<T>
    {
        int Alta(T p);

        int Baja(int id);

        int Modificacion(T p);

        List<T> TraerTodos(int paginaNro,int tamPagina);


        T TraerId(int id);

        public int ObtenerTotalPaginas(int tamPagina,int totalRegistros);

//total de filas de la entidad x
        public int TraerCantidad();

    }
}