using System.Data;
using MySql.Data.MySqlClient;

namespace StiegerModels
{
    class RepositorioInquilino : Conexion, IrepositorioInquilino
    {
        public int Alta(InquilinoModel i)
        {
            this.abrirConexion();
            int id = -1;
            string sql =
                @"INSERT INTO `inquilino`(`dni`, `nombre`, `apellido`, `telefono`, `mail`) VALUES (@dni,@nombre,@apellido,@telefono,@mail);
               SELECT LAST_INSERT_ID();";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@dni", i.Dni);
            comando.Parameters.AddWithValue("@nombre", i.Nombre);
            comando.Parameters.AddWithValue("@apellido", i.Apellido);
            comando.Parameters.AddWithValue("@telefono", i.Telefono);
            comando.Parameters.AddWithValue("@mail", i.Mail);
            comando.CommandType = CommandType.Text;
            id = Convert.ToInt32(comando.ExecuteScalar());
            i.Id_inquilino = id;
            this.cerrarConexion();

            return id;
        }

        public int Baja(int id)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql = @"UPDATE `inquilino` SET `activo` = '0' WHERE `id_inquilino` = @id;";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            comando.CommandType = CommandType.Text;
            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;
        }

        public int Modificacion(InquilinoModel i)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql =
                @"UPDATE `inquilino` SET `dni`=@dni ,`nombre`=@nombre,`apellido`=@apellido,`telefono`=@telefono,`mail`=@mail
                 WHERE `id_inquilino`=@id";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@dni", i.Dni);
            comando.Parameters.AddWithValue("@nombre", i.Nombre);
            comando.Parameters.AddWithValue("@apellido", i.Apellido);
            comando.Parameters.AddWithValue("@telefono", i.Telefono);
            comando.Parameters.AddWithValue("@mail", i.Mail);
            comando.Parameters.AddWithValue("@id", i.Id_inquilino);
            comando.CommandType = CommandType.Text;
            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;
        }

        public List<InquilinoModel> TraerTodos()
        {
            List<InquilinoModel> inquilinos = new List<InquilinoModel>();

            this.abrirConexion();
            string sql = @"SELECT * FROM inquilino WHERE `activo` = '1' ";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                InquilinoModel i = new InquilinoModel();
                i.Id_inquilino = lector.GetInt16("id_inquilino");
                i.Dni = lector.GetString("dni");
                i.Nombre = lector.GetString("nombre");
                i.Apellido = lector.GetString("apellido");
                i.Telefono = lector.IsDBNull(lector.GetOrdinal("telefono")) ? null : lector.GetString("telefono");
                i.Mail = lector.IsDBNull(lector.GetOrdinal("mail")) ? null : lector.GetString("mail");
                inquilinos.Add(i);
            }
            this.cerrarConexion();

            return inquilinos;
        }

        public InquilinoModel TraerId(int id)
        {
            this.abrirConexion();
            string sql =
                @"SELECT `id_inquilino`, `dni`, `nombre`, `apellido`, `telefono`,`mail`  
                        FROM `inquilino` WHERE `id_inquilino`=@id AND activo='1'";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();

            InquilinoModel i = new InquilinoModel();
            if (lector.Read())
            {
                i.Id_inquilino = lector.GetInt16("id_inquilino");
                i.Dni = lector.GetString("dni");
                i.Nombre = lector.GetString("nombre");
                i.Apellido = lector.GetString("apellido");
                i.Telefono = lector.IsDBNull(lector.GetOrdinal("telefono")) ? null : lector.GetString("telefono");
                i.Mail = lector.IsDBNull(lector.GetOrdinal("mail")) ? null : lector.GetString("mail");
            }
            else
            {
                i.Id_inquilino = -1;
            }
            this.cerrarConexion();

            return i;
        }
    }
}
