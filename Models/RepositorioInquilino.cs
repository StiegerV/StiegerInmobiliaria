using System.Data;
using MySql.Data.MySqlClient;
using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Models
{
    public class RepositorioInquilino : Conexion, IrepositorioInquilino
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

        public List<InquilinoModel> TraerTodos(int paginaNro, int tamPagina)
        {
            List<InquilinoModel> inquilinos = new List<InquilinoModel>();

            this.abrirConexion();
            string sql = @$"SELECT * FROM inquilino WHERE `activo` = '1' 
            LIMIT {(paginaNro - 1) * tamPagina}, {tamPagina};";
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

        public InquilinoDTO traerIdDTO(int id)
        {
            this.abrirConexion();
            string sql =
                @"SELECT `id_inquilino`, `nombre`, `apellido` FROM `inquilino` WHERE `id_inquilino`=@id AND `activo`=1";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();

            var i = new InquilinoDTO();
            if (lector.Read())
            {
                i.Id_inquilino = lector.GetInt16("id_inquilino");
                i.Nombre = lector.GetString("nombre");
                i.Apellido = lector.GetString("apellido");
            }
            this.cerrarConexion();

            return i;
        }


        public List<InquilinoModel> Busqueda(string dato)
        {
            dato = "%" + dato + "%";
            this.abrirConexion();
            string sql = @"SELECT `id_inquilino`, `nombre`,`apellido`,`dni` FROM `inquilino` 
            WHERE `activo`=1 AND `nombre` LIKE @dato 
            OR `activo`=1 AND `apellido` LIKE @dato 
            OR `activo`=1 AND `dni`LIKE @dato";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@dato", dato);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();

            List<InquilinoModel> inquilinos = new List<InquilinoModel>();

            while (lector.Read())
            {
                InquilinoModel p = new InquilinoModel();
                p.Id_inquilino = lector.GetInt16("id_inquilino");
                p.Dni = lector.GetString("dni");
                p.Nombre = lector.GetString("nombre");
                p.Apellido = lector.GetString("apellido");
                inquilinos.Add(p);
            }

            this.cerrarConexion();

            return inquilinos;
        }

        public int TraerCantidad()
        {
            this.abrirConexion();
            string sql = @"SELECT COUNT(`id_inquilino`) AS cantidad FROM `inquilino` WHERE `activo`=1";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();

            int cantidad = 0;

            if (lector.Read())
            {
                cantidad = lector.GetInt32("cantidad");

            }

            this.cerrarConexion();
            return cantidad;
        }


        public int ObtenerTotalPaginas(int tamPagina, int totalRegistros)
        {
            return (int)Math.Ceiling((double)totalRegistros / tamPagina);
        }
    }
}
