using MySql.Data.MySqlClient;
using System.Data;

namespace StiegerModels
{
    class RepositorioPropietario : Conexion, IrepositorioPropietario
    {
        public RepositorioPropietario() { }
        public int Alta(PropietarioModel p)
        {
            this.abrirConexion();
            int id = -1;
            string sql = @"INSERT INTO propietario(dni, nombre, apellido, telefono) 
               VALUES (@dni, @nombre, @apellido, @telefono);
               SELECT LAST_INSERT_ID();";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@dni", p.Dni);
            comando.Parameters.AddWithValue("@nombre", p.Nombre);
            comando.Parameters.AddWithValue("@apellido", p.Apellido);
            comando.Parameters.AddWithValue("@telefono", p.telefono);
            comando.CommandType = CommandType.Text;
            id = Convert.ToInt32(comando.ExecuteScalar());
            p.Id_propietario = id;
            this.cerrarConexion();

            return id;
        }

        public int Baja(PropietarioModel p)
        {

            return 1;
        }

        public int Modificacion(PropietarioModel p)
        {

            return 1;
        }

        public List<PropietarioModel> TraerTodos()
        {
            List<PropietarioModel> propietarios = new List<PropietarioModel>();


            return propietarios;
        }


        public PropietarioModel TraerId(int id)
        {
            PropietarioModel p = new PropietarioModel();

            return p;
        }
    }
}