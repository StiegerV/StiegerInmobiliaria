namespace StiegerModels
{
    // <T> indica que es una interfaz generica que puede recibir varios tipos de objetos

   public interface iRepositorio<T>
    {
        int Alta(T p);

        int Baja(int id);

        int Modificacion(T p);

        List<T> TraerTodos();


        T TraerId(int id);

    }
}