using MySql.Data.MySqlClient;
using System.Data;
using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Models
{
    public class RepositorioUsuario : Conexion, IrepositorioUsuario
    {
        public int Alta(UsuarioModel u)
        {
            this.abrirConexion();
            int id = -1;
            string sql =
                @"INSERT INTO `usuario`(`nombre`, `contrasena`, `rol`,`imagen`) VALUES (@nombre,@contrasena,@rol,@imagen);
               SELECT LAST_INSERT_ID();";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@nombre", u.Nombre);
            comando.Parameters.AddWithValue("@contrasena", u.Contraseña);
            comando.Parameters.AddWithValue("@rol", u.Rol);
            comando.Parameters.AddWithValue("@imagen", u.Imagen);
            comando.CommandType = CommandType.Text;
            id = Convert.ToInt32(comando.ExecuteScalar());
            u.Id_usuario = id;
            this.cerrarConexion();

            return id;
        }

        public int Baja(int id)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql = @"UPDATE `usuario` SET `activo`=0 WHERE `id_usuario`=@id";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            comando.CommandType = CommandType.Text;
            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;
        }

        public int Modificacion(UsuarioModel u)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql =
                @"UPDATE `usuario` SET `nombre`=@nombre,`contrasena`=@contrasena ,`rol`=@rol,`imagen`=@imagen WHERE `id_usuario`=@id";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", u.Id_usuario);
            comando.Parameters.AddWithValue("@nombre", u.Nombre);
            comando.Parameters.AddWithValue("@contrasena", u.Contraseña);
            comando.Parameters.AddWithValue("@rol", u.Rol);
            comando.Parameters.AddWithValue("@imagen", u.Imagen);

            comando.CommandType = CommandType.Text;
            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;
        }

        public List<UsuarioModel> TraerTodos(int paginaNro, int tamPagina)
        {
            var usuarios = new List<UsuarioModel>();

            this.abrirConexion();
            string sql = @"SELECT * FROM `usuario` WHERE `activo`=1";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                var u = new UsuarioModel();
                u.Id_usuario = lector.GetInt16("id_usuario");
                u.Nombre = lector.GetString("nombre");
                u.Rol = lector.GetString("rol");
                u.Imagen = lector.IsDBNull(lector.GetOrdinal("imagen")) ? null : lector.GetString("imagen");
                usuarios.Add(u);
            }
            this.cerrarConexion();

            return usuarios;
        }

        public UsuarioModel TraerId(int id)
        {
            this.abrirConexion();
            string sql =
                @"SELECT `id_usuario`,`nombre`,`contrasena`,`rol`,`imagen` FROM usuario WHERE id_usuario=@id and activo=1";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();

            var u = new UsuarioModel();
            if (lector.Read())
            {
                u.Id_usuario = lector.GetInt16("id_usuario");
                u.Nombre = lector.GetString("nombre");
                u.Contraseña = lector.GetString("contrasena");
                u.Rol = lector.GetString("rol");
                u.Imagen = lector.IsDBNull(lector.GetOrdinal("imagen")) ? null : lector.GetString("imagen");
            }
            else
            {
                u.Id_usuario = -1;
            }
            this.cerrarConexion();

            return u;
        }

        public UsuarioModel obtenerXNombre(string nombre)
        {
            this.abrirConexion();
            string sql =
                @"SELECT *  FROM usuario WHERE `nombre` = @nombre and activo=1";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();

            var u = new UsuarioModel();
            if (lector.Read())
            {
                u.Id_usuario = lector.GetInt16("id_usuario");
                u.Nombre = lector.GetString("nombre");
                u.Contraseña = lector.GetString("contrasena");
                u.Rol = lector.GetString("rol");
            }
            else
            {
                u.Id_usuario = -1;
            }
            this.cerrarConexion();

            return u;
        }

        public List<UsuarioDTO> TraerTodosDTO()
        {
            var usuarios = new List<UsuarioDTO>();

            this.abrirConexion();
            string sql = @"SELECT * FROM `usuario` WHERE `activo`=1";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                var u = new UsuarioDTO();
                u.Id_usuario = lector.GetInt16("id_usuario");
                u.Nombre = lector.GetString("nombre");
                u.Rol = lector.GetString("rol");
                usuarios.Add(u);
            }
            this.cerrarConexion();

            return usuarios;
        }

        public int TraerCantidad()
        {
            this.abrirConexion();
            string sql = @"SELECT COUNT(`id_usuario`) AS cantidad FROM `usuario` WHERE `activo`=1";
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