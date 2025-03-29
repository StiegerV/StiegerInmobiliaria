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

/**/
        public int Baja(int id)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql = @"UPDATE `propietario` SET activo=0 
                            WHERE `id_propietario`=@id";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            comando.CommandType = CommandType.Text;
            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;
        }


        public int Modificacion(PropietarioModel p)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql = @"UPDATE `propietario` SET`dni`=@dni,`nombre`=@nombre,`apellido`=@apellido,`telefono`=@telefono
                            WHERE `id_propietario`=@id";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@dni", p.Dni);
            comando.Parameters.AddWithValue("@nombre", p.Nombre);
            comando.Parameters.AddWithValue("@apellido", p.Apellido);
            comando.Parameters.AddWithValue("@telefono", p.telefono);
            comando.Parameters.AddWithValue("@id", p.Id_propietario);
            comando.CommandType = CommandType.Text;
            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;
        }

        public List<PropietarioModel> TraerTodos()
        {
            List<PropietarioModel> propietarios = new List<PropietarioModel>();

            this.abrirConexion();
            string sql = @"SELECT * FROM propietario WHERE activo=1";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                PropietarioModel p = new PropietarioModel();
                p.Id_propietario = lector.GetInt16("id_propietario");
                p.Dni = lector.GetString("dni");
                p.Nombre = lector.GetString("nombre");
                p.Apellido = lector.GetString("apellido");
                p.telefono = lector.GetString("telefono");

                propietarios.Add(p);
            }
            this.cerrarConexion();


            return propietarios;
        }

        //
        public PropietarioModel TraerId(int id)
        {
            this.abrirConexion();
            string sql = @"SELECT `id_propietario`, `dni`, `nombre`, `apellido`, `telefono` 
                        FROM `propietario` WHERE `id_propietario`=@id AND activo=1";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();

            PropietarioModel p = new PropietarioModel();
            if (lector.Read())
            {
                p.Id_propietario = lector.GetInt16("id_propietario");
                p.Dni = lector.GetString("dni");
                p.Nombre = lector.GetString("nombre");
                p.Apellido = lector.GetString("apellido");
                p.telefono = lector.GetString("telefono");
            }
            else
            {p.Id_propietario = -1;}
            this.cerrarConexion();

            return p;
        }
    }
}