using MySql.Data.MySqlClient;
using StiegerInmobiliaria.DTOs;
using System.Data;

namespace StiegerInmobiliaria.Models
{
    public class RepositorioContrato : Conexion, iRepositorioContrato
    {
        //---------------------------------------------------------------Interfaz base---------------------------------------------------------------
        //---------------------------------------------------------------Interfaz base---------------------------------------------------------------
        //---------------------------------------------------------------Interfaz base---------------------------------------------------------------

        public int Alta(ContratoModel c)
        {
            this.abrirConexion();
            int id = -1;
            string sql = @"INSERT INTO contrato(monto, fecha_inicio, fecha_fin, id_inmueble, id_inquilino, fecha_fin_original, creado_por) 
               VALUES (@monto, @fecha_inicio, @fecha_fin, @id_inmueble, @id_inquilino, @fecha_fin_original, @creado_por);
               SELECT LAST_INSERT_ID();";
            //formateo de fehcas para mysql
            string inicio = c.FechaInicio.ToString("yyyy-MM-dd");
            string fin = c.FechaFin.ToString("yyyy-MM-dd");

            Console.WriteLine(inicio);
            Console.WriteLine(fin);

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@monto", c.Monto);
            comando.Parameters.AddWithValue("@fecha_inicio", inicio);
            comando.Parameters.AddWithValue("@fecha_fin", fin);
            comando.Parameters.AddWithValue("@id_inmueble", c.Id_inmueble);
            comando.Parameters.AddWithValue("@id_inquilino", c.Id_inquilino);
            comando.Parameters.AddWithValue("@fecha_fin_original", c.FechaFin);
            comando.Parameters.AddWithValue("@creado_por", c.Creador_por);
            id = Convert.ToInt32(comando.ExecuteScalar());
            c.Id_contrato = id;
            this.cerrarConexion();

            return id;
        }


        public int Baja(int id)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql = @"UPDATE contrato SET terminado_por=1,activo=0 WHERE id_contrato=@id";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());
            this.cerrarConexion();
            return columnasAfectadas;
        }


        public int Modificacion(ContratoModel c)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql = @"UPDATE `contrato` 
                            SET monto=@monto,fecha_inicio=@fecha_inicio,fecha_fin=@fecha_fin,
                            id_inmueble=@id_inmueble,id_inquilino=@id_inquilino
                            WHERE id_contrato=@id_contrato";
            string inicio = c.FechaInicio.ToString("yyyy-MM-dd");
            string fin = c.FechaFin.ToString("yyyy-MM-dd");
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@monto", c.Monto);
            comando.Parameters.AddWithValue("@fecha_inicio", inicio);
            comando.Parameters.AddWithValue("@fecha_fin", fin);
            comando.Parameters.AddWithValue("@id_inmueble", c.Id_inmueble);
            comando.Parameters.AddWithValue("@id_inquilino", c.Id_inquilino);
            comando.Parameters.AddWithValue("@id_contrato", c.Id_contrato);

            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;

        }

        public int Cancelar(ContratoModel c)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql = @"UPDATE `contrato` 
                            SET fecha_fin=@fecha_fin,`terminado_por`=@terminado_por
                            WHERE id_contrato=@id_contrato";
            string inicio = c.FechaInicio.ToString("yyyy-MM-dd");
            string fin = c.FechaFin.ToString("yyyy-MM-dd");
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@fecha_fin", fin);
            comando.Parameters.AddWithValue("@terminado_por", c.Terminado_por);
            comando.Parameters.AddWithValue("@id_contrato", c.Id_contrato);

            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;

        }

        public List<ContratoModel> TraerTodos(int paginaNro, int tamPagina)
        {
            var contratos = new List<ContratoModel>();
            this.abrirConexion();
            string sql = "SELECT * FROM `contrato` WHERE `activo`=1";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                var c = new ContratoModel();
                c.Id_contrato = lector.GetInt16("id_contrato");
                c.Monto = lector.GetFloat("monto");
                c.FechaInicio = lector.GetDateTime("fecha_inicio");
                c.FechaFin = lector.GetDateTime("fecha_fin");
                c.Id_inmueble = lector.GetInt16("id_inmueble");
                c.Id_inquilino = lector.GetInt16("id_inquilino");
                c.Creador_por = lector.GetInt16("creado_por");
                c.Terminado_por = lector.IsDBNull(lector.GetOrdinal("terminado_por")) ? null : lector.GetInt16("terminado_por");
                c.Fecha_fin_original = lector.IsDBNull(lector.GetOrdinal("fecha_fin_original")) ? null : lector.GetDateTime("fecha_fin_original");
                contratos.Add(c);
            }
            this.cerrarConexion();
            return contratos;
        }

        //---------------------------------------------------------------ESPECIFICO---------------------------------------------------------------
        //---------------------------------------------------------------ESPECIFICO---------------------------------------------------------------
        //---------------------------------------------------------------ESPECIFICO---------------------------------------------------------------

        public ContratoModel TraerId(int id)
        {
            this.abrirConexion();
            string sql = @"SELECT `id_contrato`, `monto`, `fecha_inicio`, `fecha_fin`, `id_inmueble`, `id_inquilino`, `creado_por`, `terminado_por` FROM `contrato` 
            WHERE `id_contrato`=@id AND`activo`=1";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            var lector = comando.ExecuteReader();
            var c = new ContratoModel();
            if (lector.Read())
            {
                c.Id_contrato = lector.GetInt16("id_contrato");
                c.Monto = lector.GetFloat("monto");
                c.FechaInicio = lector.GetDateTime("fecha_inicio");
                c.FechaFin = lector.GetDateTime("fecha_fin");
                c.Id_inmueble = lector.GetInt16("id_inmueble");
                c.Id_inquilino = lector.GetInt16("id_inquilino");
                c.Creador_por = lector.GetInt16("creado_por");
                c.Terminado_por = lector.IsDBNull(lector.GetOrdinal("terminado_por")) ? null : lector.GetInt16("terminado_por");

            }
            else
            {
                c.Id_contrato = -1;
            }
            this.cerrarConexion();
            return c;
        }


        public List<ContratoJSON> Busqueda(string dato)
        {
            dato = "%" + dato + "%";
            this.abrirConexion();
            string sql = @"SELECT `id_contrato`,c.`id_inmueble`,m.tipo,c.id_inquilino,i.apellido FROM `contrato` as c
                        JOIN inmueble as m ON m.id_inmueble=c.`id_inmueble`
                        JOIN inquilino as i ON i.id_inquilino=c.`id_inquilino`
                        WHERE tipo LIKE @dato OR apellido LIKE @dato";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@dato", dato);
            var lector = comando.ExecuteReader();

            var contratos = new List<ContratoJSON>();

            while (lector.Read())
            {
                var c = new ContratoJSON();
                c.Id_contrato = lector.GetInt16("id_contrato");
                c.Id_inmueble = lector.GetInt16("id_inmueble");
                c.Tipo_inmueble = lector.GetString("tipo");
                c.Id_inquilino = lector.GetInt16("id_inquilino");
                c.Apellido_inquilino = lector.GetString("apellido");
                contratos.Add(c);
            }

            this.cerrarConexion();

            return contratos;
        }

        //---------------------------------------------------------------PAGINADO---------------------------------------------------------------
        //---------------------------------------------------------------PAGINADO---------------------------------------------------------------
        //---------------------------------------------------------------PAGINADO---------------------------------------------------------------
        public int TraerCantidad()
        {
            this.abrirConexion();
            string sql = @"SELECT COUNT(`id_contrato`) AS cantidad FROM `contrato` WHERE `activo`=1";
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


        public List<ContratoDTO> TraerTodosDTO(int paginaNro, int tamPagina)
        {
            var listaContratos = new List<ContratoDTO>();
            this.abrirConexion();
            string sql = @$"
            SELECT c.id_contrato, c.fecha_inicio, c.fecha_fin, c.fecha_fin_original,
            i.id_inquilino, i.nombre AS inquilino_nombre, i.apellido AS inquilino_apellido,
            m.id_inmueble, m.tipo, m.direccion,
            p.id_propietario, p.nombre AS propietario_nombre, p.apellido AS propietario_apellido
            FROM contrato AS c
            JOIN inquilino AS i ON i.id_inquilino = c.id_inquilino
            JOIN inmueble AS m ON m.id_inmueble = c.id_inmueble
            JOIN propietario AS p ON p.id_propietario = m.id_propietario
            WHERE c.activo = 1
            LIMIT {(paginaNro - 1) * tamPagina}, {tamPagina};";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                var c = new ContratoDTO();
                c.Id_contrato = lector.GetInt16("id_contrato");
                c.Fecha_inicio = lector.GetDateTime("fecha_inicio");
                c.Fecha_fin = lector.GetDateTime("fecha_fin");
                c.Fecha_fin_original = lector.IsDBNull(lector.GetOrdinal("fecha_fin_original")) ? null : lector.GetDateTime("fecha_fin_original");
                var i = new InquilinoDTO();
                i.Id_inquilino = lector.GetInt16("id_inquilino");
                i.Nombre = lector.GetString("inquilino_nombre");
                i.Apellido = lector.GetString("inquilino_apellido");
                c.Inquilino = i;
                var m = new InmuebleDTO();
                m.Id_inmueble = lector.GetInt16("id_inmueble");
                m.Tipo = lector.GetString("tipo");
                m.Direccion = lector.GetString("direccion");
                c.Inmueble = m;
                var p = new PropietarioDTO();
                p.Id_propietario = lector.GetInt16("id_propietario");
                p.Nombre = lector.GetString("propietario_nombre");
                p.Apellido = lector.GetString("propietario_apellido");
                c.Propietario = p;
                listaContratos.Add(c);
            }
            this.cerrarConexion();
            return listaContratos;
        }

        //----------------------------------------------------------------FILTROS--------------------------------------------------------------
        //----------------------------------------------------------------FILTROS--------------------------------------------------------------
        //----------------------------------------------------------------FILTROS--------------------------------------------------------------
        public List<ContratoDTO> BuscarVigenteXFechas(int paginaNro, int tamPagina, string inicio, string fin)
        {
            var listaContratos = new List<ContratoDTO>();
            this.abrirConexion();
            string sql = @$"
            SELECT c.id_contrato, c.fecha_inicio, c.fecha_fin, c.fecha_fin_original,
            i.id_inquilino, i.nombre AS inquilino_nombre, i.apellido AS inquilino_apellido,
            m.id_inmueble, m.tipo, m.direccion,
            p.id_propietario, p.nombre AS propietario_nombre, p.apellido AS propietario_apellido
            FROM contrato AS c
            JOIN inquilino AS i ON i.id_inquilino = c.id_inquilino
            JOIN inmueble AS m ON m.id_inmueble = c.id_inmueble
            JOIN propietario AS p ON p.id_propietario = m.id_propietario
            WHERE c.fecha_inicio>=@inicio AND c.fecha_fin<=@fin AND c.activo=1
            LIMIT {(paginaNro - 1) * tamPagina}, {tamPagina};";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@inicio", inicio);
            comando.Parameters.AddWithValue("@fin", fin);
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                var c = new ContratoDTO();
                c.Id_contrato = lector.GetInt16("id_contrato");
                c.Fecha_inicio = lector.GetDateTime("fecha_inicio");
                c.Fecha_fin = lector.GetDateTime("fecha_fin");
                c.Fecha_fin_original = lector.IsDBNull(lector.GetOrdinal("fecha_fin_original")) ? null : lector.GetDateTime("fecha_fin_original");
                var i = new InquilinoDTO();
                i.Id_inquilino = lector.GetInt16("id_inquilino");
                i.Nombre = lector.GetString("inquilino_nombre");
                i.Apellido = lector.GetString("inquilino_apellido");
                c.Inquilino = i;
                var m = new InmuebleDTO();
                m.Id_inmueble = lector.GetInt16("id_inmueble");
                m.Tipo = lector.GetString("tipo");
                m.Direccion = lector.GetString("direccion");
                c.Inmueble = m;
                var p = new PropietarioDTO();
                p.Id_propietario = lector.GetInt16("id_propietario");
                p.Nombre = lector.GetString("propietario_nombre");
                p.Apellido = lector.GetString("propietario_apellido");
                c.Propietario = p;
                listaContratos.Add(c);
            }
            this.cerrarConexion();
            return listaContratos;
        }


        public List<ContratoDTO> ContratoXInmueble(int id_inmueble)
        {
            var listaContratos = new List<ContratoDTO>();
            this.abrirConexion();
            string sql = @$"
            SELECT c.id_contrato, c.fecha_inicio, c.fecha_fin, c.fecha_fin_original,
            i.id_inquilino, i.nombre AS inquilino_nombre, i.apellido AS inquilino_apellido
            FROM contrato AS c
            JOIN inquilino AS i ON i.id_inquilino = c.id_inquilino
            WHERE c.activo = 1 AND `id_inmueble`=@id";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id_inmueble);
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                var c = new ContratoDTO();
                c.Id_contrato = lector.GetInt16("id_contrato");
                c.Fecha_inicio = lector.GetDateTime("fecha_inicio");
                c.Fecha_fin = lector.GetDateTime("fecha_fin");
                c.Fecha_fin_original = lector.IsDBNull(lector.GetOrdinal("fecha_fin_original")) ? null : lector.GetDateTime("fecha_fin_original");
                var i = new InquilinoDTO();
                i.Id_inquilino = lector.GetInt16("id_inquilino");
                i.Nombre = lector.GetString("inquilino_nombre");
                i.Apellido = lector.GetString("inquilino_apellido");
                c.Inquilino = i;
                listaContratos.Add(c);
            }
            this.cerrarConexion();
            return listaContratos;
        }
    }

}