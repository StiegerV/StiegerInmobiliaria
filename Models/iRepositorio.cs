namespace StiegerModels
{
    // <T> indica que es una interfaz generica que puede recibir varios tipos de objetos
    interface iRepositorio<T>
    {
        public int Alta(T p);

        public int Baja(T p);

        public int Modificacion(T p);

        public List<T> TraerTodos();


        public T TraerId(int id);

    }
}