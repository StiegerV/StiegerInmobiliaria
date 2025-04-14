using MySql.Data.MySqlClient;

namespace StiegerInmobiliaria.Models
{
    public class Conexion
    {
        private string connectionString = "server=localhost;uid=root;pwd=;database=stieger_inmobiliaria";
        public MySqlConnection conexionsql{get;set;}

        // Constructor should be public to allow instantiation from outside
        public Conexion()
        {
            conexionsql = new MySqlConnection(connectionString);
        }

        // Method to open the connection if it's not already open
        public void abrirConexion()
        {
            if (conexionsql.State == System.Data.ConnectionState.Closed)
            {
                conexionsql.Open();
            }
        }

        // Method to close the connection
        public void cerrarConexion()
        {
            if (conexionsql.State == System.Data.ConnectionState.Open)
            {
                conexionsql.Close();
            }
        }
    }
}
