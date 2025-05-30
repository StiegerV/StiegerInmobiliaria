using System.Data;
using MySql.Data.MySqlClient;
using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Models
{
    public class RepositorioInmueble : Conexion, IrepositorioInmueble
    {

        //---------------------------------------------------------------Interfaz base---------------------------------------------------------------
        //---------------------------------------------------------------Interfaz base---------------------------------------------------------------
        //---------------------------------------------------------------Interfaz base---------------------------------------------------------------

        public int Alta(InmuebleModel i)
        {
            this.abrirConexion();
            int id = -1;
            string sql = @"INSERT INTO `inmueble`
            ( `id_propietario`, `direccion`, `uso`, `tipo`, `ambientes`, `cordenadas`, `precio`, `disponible`, `imagen`) 
            VALUES (@id_propietario,@direccion,@uso,@tipo,@ambientes,@cordenadas,@precio,@disponible,@imagen)";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id_propietario", i.Id_propietario);
            comando.Parameters.AddWithValue("@direccion", i.Direccion);
            comando.Parameters.AddWithValue("@uso", i.Uso);
            comando.Parameters.AddWithValue("@tipo", i.Tipo);
            comando.Parameters.AddWithValue("@ambientes", i.Ambientes);
            comando.Parameters.AddWithValue("@cordenadas", i.Cordenadas);
            comando.Parameters.AddWithValue("@precio", i.Precio);
            comando.Parameters.AddWithValue("@disponible", i.Disponible);
            comando.Parameters.AddWithValue("@imagen", i.Imagen);
            comando.CommandType = CommandType.Text;
            id = Convert.ToInt32(comando.ExecuteScalar());
            i.Id_inmueble = id;
            this.cerrarConexion();

            return id;

        }

        public int Baja(int id)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql = @"UPDATE inmueble SET `activo`=0 WHERE `id_inmueble`=@id_inmueble";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id_inmueble", id);
            comando.CommandType = CommandType.Text;
            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());
            this.cerrarConexion();
            return columnasAfectadas;

        }

        public int Modificacion(InmuebleModel i)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql = @"
    UPDATE inmueble 
    SET 
        id_propietario = @id_propietario,
        direccion = @direccion,
        uso = @uso,
        tipo = @tipo,
        ambientes = @ambientes,
        cordenadas = @cordenadas,
        precio = @precio,
        disponible = @disponible,
        imagen = @imagen
    WHERE id_inmueble = @id_inmueble";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id_propietario", i.Id_propietario);
            comando.Parameters.AddWithValue("@direccion", i.Direccion);
            comando.Parameters.AddWithValue("@uso", i.Uso);
            comando.Parameters.AddWithValue("@tipo", i.Tipo);
            comando.Parameters.AddWithValue("@ambientes", i.Ambientes);
            comando.Parameters.AddWithValue("@cordenadas", i.Cordenadas);
            comando.Parameters.AddWithValue("@precio", i.Precio);
            comando.Parameters.AddWithValue("@disponible", i.Disponible);
            comando.Parameters.AddWithValue("@imagen", i.Imagen);
            comando.Parameters.AddWithValue("@Id_inmueble", i.Id_inmueble);

            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;

        }

        public List<InmuebleModel> TraerTodos(int paginaNro, int tamPagina)
        {
            List<InmuebleModel> inmuebles = new List<InmuebleModel>();
            this.abrirConexion();
            string sql = @$"SELECT * FROM `inmueble` WHERE `activo`=1
            LIMIT {(paginaNro - 1) * tamPagina}, {tamPagina};";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                InmuebleModel i = new InmuebleModel();
                i.Id_inmueble = lector.GetInt16("id_inmueble");
                i.Id_propietario = lector.GetInt16("id_propietario");
                i.Direccion = lector.IsDBNull(lector.GetOrdinal("direccion")) ? null : lector.GetString("direccion");
                i.Uso = lector.IsDBNull(lector.GetOrdinal("uso")) ? null : lector.GetString("uso");
                i.Tipo = lector.IsDBNull(lector.GetOrdinal("tipo")) ? null : lector.GetString("tipo");
                i.Ambientes = lector.IsDBNull(lector.GetOrdinal("ambientes")) ? -1 : lector.GetInt32("ambientes");
                i.Cordenadas = lector.IsDBNull(lector.GetOrdinal("cordenadas")) ? null : lector.GetString("cordenadas");
                i.Precio = lector.IsDBNull(lector.GetOrdinal("precio")) ? -1 : lector.GetDouble("precio");
                i.Disponible = lector.IsDBNull(lector.GetOrdinal("disponible")) ? null : lector.GetString("disponible");
                i.Imagen = lector.IsDBNull(lector.GetOrdinal("imagen")) ? null : lector.GetString("imagen");

                inmuebles.Add(i);
            }
            lector.Close();
            this.cerrarConexion();
            return inmuebles;
        }


        public int EliminarImagen(InmuebleModel i)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql = @"UPDATE inmueble SET imagen='' WHERE id_inmueble=@id";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", i.Id_inmueble);

            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;
        }

        //---------------------------------------------------------------ESPECIFICO---------------------------------------------------------------
        //---------------------------------------------------------------ESPECIFICO---------------------------------------------------------------
        //---------------------------------------------------------------ESPECIFICO---------------------------------------------------------------

        /// <summary>
        /// permite conseguir el dto del propietario a traves del inmueble id asociado al inmueble
        /// </summary>
        /// <param name="id">el id del inmueble del que necesitamos el propietario.</param>
        public PropietarioDTO TraerIdPropietarioDTO(int id)
        {
            this.abrirConexion();
            string sql = @"SELECT p.id_propietario, p.nombre, p.apellido
                        FROM inmueble AS i
                        INNER JOIN propietario AS p ON i.id_propietario = p.id_propietario
                        WHERE i.id_inmueble = @id";
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
            lector.Close();

            this.cerrarConexion();
            return p;
        }


        public InmuebleDTO TraerIdDTO(int id)
        {
            this.abrirConexion();
            string sql = @"SELECT `id_inmueble`,`tipo`,`direccion` FROM `inmueble` WHERE `id_inmueble`=@id";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();

            var i = new InmuebleDTO();

            if (lector.Read())
            {
                i.Id_inmueble = lector.GetInt16("id_inmueble");
                i.Tipo = lector.GetString("tipo");
                i.Direccion = lector.GetString("direccion");
            }
            lector.Close();
            this.cerrarConexion();
            return i;
        }
        public List<InmuebleDTO> traerDesocupados(string inicio, string fin)
        {
            var desocupados = new List<InmuebleDTO>();
            this.abrirConexion();

            string sql = @"
        SELECT i.id_inmueble, i.tipo, i.direccion, i.precio
        FROM inmueble AS i
        WHERE i.id_inmueble NOT IN (
            SELECT c.id_inmueble
            FROM contrato AS c
            WHERE c.fecha_inicio <= @fechaFin
              AND c.fecha_fin >= @fechaInicio
              AND activo = 1
        )";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@fechaInicio", inicio);
            comando.Parameters.AddWithValue("@fechaFin", fin);

            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                var i = new InmuebleDTO
                {
                    Id_inmueble = lector.GetInt16("id_inmueble"),
                    Tipo = lector.GetString("tipo"),
                    Direccion = lector.GetString("direccion"),
                    Monto = lector.GetFloat("precio")
                };

                desocupados.Add(i);
            }

            lector.Close();
            this.cerrarConexion();
            return desocupados;
        }


        public int ContratoActivo(int id)
        {
            int id_contrato = -1;
            this.abrirConexion();
            string sql = @"SELECT `id_contrato` FROM `contrato` WHERE `id_inmueble`=@id";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            var lector = comando.ExecuteReader();
            if (lector.Read())
            {
                id_contrato = lector.GetInt16("id_contrato");
            }
            lector.Close();
            this.cerrarConexion();
            return id_contrato;
        }
        public InmuebleModel TraerId(int id)
        {
            this.abrirConexion();
            string sql = @"SELECT `id_propietario`, `direccion`, `uso`, `tipo`, `ambientes`, `cordenadas`, `precio`, `disponible`, `activo`, `imagen` 
      FROM `inmueble` WHERE `id_inmueble`=@id_inmueble";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id_inmueble", id);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            InmuebleModel i = new InmuebleModel();
            if (lector.Read())
            {
                i.Id_inmueble = id;
                i.Id_propietario = lector.GetInt16("id_propietario");
                i.Direccion = lector.IsDBNull(lector.GetOrdinal("direccion")) ? null : lector.GetString("direccion");
                i.Uso = lector.IsDBNull(lector.GetOrdinal("uso")) ? null : lector.GetString("uso");
                i.Tipo = lector.IsDBNull(lector.GetOrdinal("tipo")) ? null : lector.GetString("tipo");
                i.Ambientes = lector.IsDBNull(lector.GetOrdinal("ambientes")) ? -1 : lector.GetInt32("ambientes");
                i.Cordenadas = lector.IsDBNull(lector.GetOrdinal("cordenadas")) ? null : lector.GetString("cordenadas");
                i.Precio = lector.IsDBNull(lector.GetOrdinal("precio")) ? -1 : lector.GetDouble("precio");
                i.Disponible = lector.IsDBNull(lector.GetOrdinal("disponible")) ? null : lector.GetString("disponible");
                i.Imagen = lector.IsDBNull(lector.GetOrdinal("imagen")) ? null : lector.GetString("imagen");
            }
            else
            {
                i.Id_inmueble = -1;
            }
            lector.Close();
            this.cerrarConexion();
            return i;
        }

        //---------------------------------------------------------------PAGINADO---------------------------------------------------------------
        //---------------------------------------------------------------PAGINADO---------------------------------------------------------------
        //---------------------------------------------------------------PAGINADO---------------------------------------------------------------
        public int TraerCantidad()
        {
            this.abrirConexion();
            string sql = @"SELECT COUNT(`id_inmueble`) AS cantidad FROM `inmueble` WHERE `activo`=1";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();

            int cantidad = 0;

            if (lector.Read())
            {
                cantidad = lector.GetInt32("cantidad");

            }
            lector.Close();
            this.cerrarConexion();
            return cantidad;
        }

        public int TraerCantidadxEstado(string estado)
        {
            this.abrirConexion();
            string sql = @"SELECT COUNT(`id_inmueble`) AS cantidad FROM `inmueble` WHERE `activo`=1 AND `disponible`=@estado";
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

        //----------------------------------------------------------------FILTROS--------------------------------------------------------------
        //----------------------------------------------------------------FILTROS--------------------------------------------------------------
        //----------------------------------------------------------------FILTROS--------------------------------------------------------------
        public List<InmuebleDTO> InmueblesxPropietario(int id)
        {
            var lista = new List<InmuebleDTO>();
            var sql = "SELECT `id_inmueble`,`tipo`,`direccion`,`precio` FROM `inmueble` WHERE `id_propietario`=@id";
            this.abrirConexion();
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);

            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                var i = new InmuebleDTO
                {
                    Id_inmueble = lector.GetInt16("id_inmueble"),
                    Tipo = lector.GetString("tipo"),
                    Direccion = lector.GetString("direccion"),
                    Monto = lector.GetFloat("precio")
                };

                lista.Add(i);
            }

            this.cerrarConexion();
            return lista;
        }


        public List<InmuebleModel> ListarXDisponible(string estado, int paginaNro, int tamPagina)
        {
            List<InmuebleModel> inmuebles = new List<InmuebleModel>();
            this.abrirConexion();
            string sql = @$"SELECT * FROM `inmueble` WHERE `disponible`=@estado AND `activo`=1
            LIMIT {(paginaNro - 1) * tamPagina}, {tamPagina};";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@estado", estado);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                InmuebleModel i = new InmuebleModel();
                i.Id_inmueble = lector.GetInt16("id_inmueble");
                i.Id_propietario = lector.GetInt16("id_propietario");
                i.Direccion = lector.IsDBNull(lector.GetOrdinal("direccion")) ? null : lector.GetString("direccion");
                i.Uso = lector.IsDBNull(lector.GetOrdinal("uso")) ? null : lector.GetString("uso");
                i.Tipo = lector.IsDBNull(lector.GetOrdinal("tipo")) ? null : lector.GetString("tipo");
                i.Ambientes = lector.IsDBNull(lector.GetOrdinal("ambientes")) ? -1 : lector.GetInt32("ambientes");
                i.Cordenadas = lector.IsDBNull(lector.GetOrdinal("cordenadas")) ? null : lector.GetString("cordenadas");
                i.Precio = lector.IsDBNull(lector.GetOrdinal("precio")) ? -1 : lector.GetDouble("precio");
                i.Disponible = lector.IsDBNull(lector.GetOrdinal("disponible")) ? null : lector.GetString("disponible");
                i.Imagen = lector.IsDBNull(lector.GetOrdinal("imagen")) ? null : lector.GetString("imagen");

                inmuebles.Add(i);
            }
            this.cerrarConexion();
            return inmuebles;



        }


        public List<InmuebleModel> ListarDesocupadoXFechas(string inicio, string fin, int paginaNro, int tamPagina)
        {
            List<InmuebleModel> inmuebles = new List<InmuebleModel>();
            this.abrirConexion();
            string sql = @$"SELECT *
                    FROM inmueble AS i
                    WHERE i.id_inmueble NOT IN (
                        SELECT c.id_inmueble
                        FROM contrato AS c
                        WHERE c.fecha_inicio <= @fechaFin
                        AND c.fecha_fin >= @fechaInicio
                        AND activo = 1)
                        LIMIT {(paginaNro - 1) * tamPagina}, {tamPagina};";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@fechaInicio", inicio);
            comando.Parameters.AddWithValue("@fechaFin", fin);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                InmuebleModel i = new InmuebleModel();
                i.Id_inmueble = lector.GetInt16("id_inmueble");
                i.Id_propietario = lector.GetInt16("id_propietario");
                i.Direccion = lector.IsDBNull(lector.GetOrdinal("direccion")) ? null : lector.GetString("direccion");
                i.Uso = lector.IsDBNull(lector.GetOrdinal("uso")) ? null : lector.GetString("uso");
                i.Tipo = lector.IsDBNull(lector.GetOrdinal("tipo")) ? null : lector.GetString("tipo");
                i.Ambientes = lector.IsDBNull(lector.GetOrdinal("ambientes")) ? -1 : lector.GetInt32("ambientes");
                i.Cordenadas = lector.IsDBNull(lector.GetOrdinal("cordenadas")) ? null : lector.GetString("cordenadas");
                i.Precio = lector.IsDBNull(lector.GetOrdinal("precio")) ? -1 : lector.GetDouble("precio");
                i.Disponible = lector.IsDBNull(lector.GetOrdinal("disponible")) ? null : lector.GetString("disponible");
                i.Imagen = lector.IsDBNull(lector.GetOrdinal("imagen")) ? null : lector.GetString("imagen");

                inmuebles.Add(i);
            }
            this.cerrarConexion();
            return inmuebles;

        }



    }

}