using MySql.Data.MySqlClient;
using System.Data;
using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Models
{
    class RepositorioPropietario : Conexion, IrepositorioPropietario
    {
        public RepositorioPropietario() { }
        public int Alta(PropietarioModel p)
        {
            this.abrirConexion();
            int id = -1;
            string sql = @"INSERT INTO propietario(dni, nombre, apellido, telefono,mail) 
               VALUES (@dni, @nombre, @apellido, @telefono,@mail);
               SELECT LAST_INSERT_ID();";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@dni", p.Dni);
            comando.Parameters.AddWithValue("@nombre", p.Nombre);
            comando.Parameters.AddWithValue("@apellido", p.Apellido);
            comando.Parameters.AddWithValue("@telefono", p.Telefono);
            comando.Parameters.AddWithValue("@mail", p.Mail);
            comando.CommandType = CommandType.Text;
            id = Convert.ToInt32(comando.ExecuteScalar());
            p.Id_propietario = id;
            this.cerrarConexion();

            return id;
        }

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
            string sql = @"UPDATE `propietario` SET`dni`=@dni,`nombre`=@nombre,`apellido`=@apellido,`telefono`=@telefono,`mail`=@mail
                            WHERE `id_propietario`=@id";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@dni", p.Dni);
            comando.Parameters.AddWithValue("@nombre", p.Nombre);
            comando.Parameters.AddWithValue("@apellido", p.Apellido);
            comando.Parameters.AddWithValue("@telefono", p.Telefono);
            comando.Parameters.AddWithValue("@mail", p.Mail);
            comando.Parameters.AddWithValue("@id", p.Id_propietario);
            comando.CommandType = CommandType.Text;
            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;
        }

        public List<PropietarioModel> TraerTodos(int paginaNro, int tamPagina)
        {
            List<PropietarioModel> propietarios = new List<PropietarioModel>();

            this.abrirConexion();
            string sql = @$"SELECT * FROM propietario WHERE activo=1
            LIMIT {(paginaNro - 1) * tamPagina}, {tamPagina};";
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
                p.Telefono = lector.IsDBNull(lector.GetOrdinal("telefono")) ? null : lector.GetString("telefono");
                p.Mail = lector.IsDBNull(lector.GetOrdinal("mail")) ? null : lector.GetString("mail");
                propietarios.Add(p);
            }
            this.cerrarConexion();


            return propietarios;
        }

        //
        public PropietarioModel TraerId(int id)
        {
            this.abrirConexion();
            string sql = @"SELECT `id_propietario`, `dni`, `nombre`, `apellido`, `telefono`,`mail`
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
                p.Telefono = lector.IsDBNull(lector.GetOrdinal("telefono")) ? null : lector.GetString("telefono");
                p.Mail = lector.IsDBNull(lector.GetOrdinal("mail")) ? null : lector.GetString("mail");
            }
            else
            { p.Id_propietario = -1; }
            this.cerrarConexion();

            return p;
        }

        public List<PropietarioModel> Busqueda(string dato)
        {
            dato = "%" + dato + "%";
            this.abrirConexion();
            string sql = @"SELECT `id_propietario`, `nombre`,`apellido`,`dni` FROM `propietario` 
            WHERE `activo`=1 AND `nombre` LIKE @dato 
            OR `activo`=1 AND `apellido` LIKE @dato 
            OR `activo`=1 AND `dni`LIKE @dato";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@dato", dato);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();

            List<PropietarioModel> propietarios = new List<PropietarioModel>();

            while (lector.Read())
            {
                PropietarioModel p = new PropietarioModel();
                p.Id_propietario = lector.GetInt16("id_propietario");
                p.Dni = lector.GetString("dni");
                p.Nombre = lector.GetString("nombre");
                p.Apellido = lector.GetString("apellido");
                propietarios.Add(p);
            }

            this.cerrarConexion();

            return propietarios;
        }


        public PropietarioDTO traerIdDTO(int id)
        {
            this.abrirConexion();
            string sql = @"SELECT `id_propietario`, `nombre`, `apellido` FROM `propietario` WHERE `id_propietario`=@id AND activo=1";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();

            var p = new PropietarioDTO();
            if (lector.Read())
            {
                p.Id_propietario = lector.GetInt16("id_propietario");
                p.Nombre = lector.GetString("nombre");
                p.Apellido = lector.GetString("apellido");
            }
            else
            { p.Id_propietario = -1; }
            this.cerrarConexion();

            return p;
        }

        public int TraerCantidad()
        {
            this.abrirConexion();
            string sql = @"SELECT COUNT(`id_propietario`) AS cantidad FROM `propietario` WHERE `activo`=1";
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