using System.Data;
using MySql.Data.MySqlClient;
using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Models
{
    public class RepositorioPago : Conexion, IrepositorioPagos
    {
        public int Alta(PagoModel p)
        {
            this.abrirConexion();
            int id = -1;
            string sql =
                @"INSERT INTO `pago`(`id_contrato`, `monto`, `fecha`, `observacion`, `estado`,`creado_por`)
                VALUES (@idContrato,@monto,@fecha,@observacion,@estado,@creado_por);
               SELECT LAST_INSERT_ID();";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);

            comando.Parameters.AddWithValue("@idContrato", p.Id_contrato);
            comando.Parameters.AddWithValue("@monto", p.Monto);
            comando.Parameters.AddWithValue("@fecha", p.Fecha);
            comando.Parameters.AddWithValue("@observacion", p.Observacion);
            comando.Parameters.AddWithValue("@estado", p.Estado);
            comando.Parameters.AddWithValue("@creado_por", p.Creado_por);
            comando.CommandType = CommandType.Text;
            id = Convert.ToInt32(comando.ExecuteScalar());
            p.Id_pago = id;
            this.cerrarConexion();

            return id;
        }

        public int BajaUser(int id, int usr)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql = @"UPDATE `pago` SET `estado`='anulado',`terminado_por`=@terminado_por WHERE `id_pago`=@id";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@terminado_por", usr);
            comando.CommandType = CommandType.Text;
            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;
        }



        public int Baja(int id)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql = @"UPDATE `pago` SET `estado`='anulado' WHERE `id_pago`=@id";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            comando.CommandType = CommandType.Text;
            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;
        }

        public int Modificacion(PagoModel p)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql =
                @"UPDATE `pago` SET `observacion`=@observacion,`estado`=@estado
                 WHERE `id_pago`=@id";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@observacion", p.Observacion);
            comando.Parameters.AddWithValue("@estado", p.Estado);
            comando.Parameters.AddWithValue("@id", p.Id_pago);

            comando.CommandType = CommandType.Text;
            columnasAfectadas = Convert.ToInt32(comando.ExecuteNonQuery());

            this.cerrarConexion();

            return columnasAfectadas;
        }

        public List<PagoModel> TraerTodos(int paginaNro, int tamPagina)
        {
            var pagos = new List<PagoModel>();

            this.abrirConexion();
            string sql = @"SELECT * FROM `pago` ";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                PagoModel p = new PagoModel();
                p.Id_pago = lector.GetInt16("id_pago");
                p.Id_contrato = lector.GetInt16("id_contrato");
                p.Monto = lector.GetDouble("monto");
                p.Fecha = lector.GetDateTime("fecha");
                p.Observacion = lector.IsDBNull(lector.GetOrdinal("observacion")) ? null : lector.GetString("observacion");
                p.Estado = lector.IsDBNull(lector.GetOrdinal("estado")) ? null : lector.GetString("estado");
                pagos.Add(p);
            }
            this.cerrarConexion();

            return pagos;
        }


        public PagoModel TraerId(int id)
        {
            this.abrirConexion();
            string sql =
                @"SELECT `id_pago`, `id_contrato`, `monto`, `fecha`, `observacion`, `estado` FROM `pago`
                 WHERE `id_pago`=@id";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            var p = new PagoModel();
            if (lector.Read())
            {
                p.Id_pago = lector.GetInt16("id_pago");
                p.Id_contrato = lector.GetInt16("id_contrato");
                p.Monto = lector.GetDouble("monto");
                p.Fecha = lector.GetDateTime("fecha");
                p.Observacion = lector.IsDBNull(lector.GetOrdinal("observacion")) ? null : lector.GetString("observacion");
                p.Estado = lector.IsDBNull(lector.GetOrdinal("estado")) ? null : lector.GetString("estado");
            }
            else
            {
                p.Id_pago = -1;
            }
            this.cerrarConexion();

            return p;
        }

        public PagoDTO traerIdDTO(int id)
        {
            this.abrirConexion();
            string sql =
                @"SELECT `id_pago`,c.id_contrato,p.monto,`fecha`,`observacion`,`estado`,i.id_inquilino,i.apellido,m.id_inmueble ,m.tipo
                    FROM `pago` as p
                    JOIN contrato as c ON p.id_contrato=c.id_contrato
                    JOIN inquilino as i on c.id_inquilino=i.id_inquilino
                    JOIN inmueble as m on c.id_inmueble=m.id_inmueble
                    where id_pago=@id";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            var p = new PagoDTO();
            if (lector.Read())
            {
                p.Id_pago = lector.GetInt16("id_pago");
                p.Id_contrato = lector.GetInt16("id_contrato");
                p.Monto = lector.GetDouble("monto");
                p.Fecha = lector.GetDateTime("fecha");
                p.Observacion = lector.IsDBNull(lector.GetOrdinal("observacion")) ? null : lector.GetString("observacion");
                p.Estado = lector.IsDBNull(lector.GetOrdinal("estado")) ? null : lector.GetString("estado");
                p.Id_inquilino = lector.GetInt16("id_inquilino");
                p.Apellido = lector.GetString("apellido");
                p.Id_inmueble = lector.GetInt16("id_inmueble");
                p.Tipo = lector.GetString("tipo");
            }
            else
            {
                p.Id_pago = -1;
            }
            this.cerrarConexion();

            return p;
        }

        public List<PagoDTO> TraerTodosDTO(int paginaNro, int tamPagina)
        {
            var pagos = new List<PagoDTO>();

            this.abrirConexion();
            string sql =
                @$"SELECT `id_pago`,c.id_contrato,p.monto,`fecha`,`observacion`,`estado`,i.id_inquilino,i.apellido,m.id_inmueble ,m.tipo
                    FROM `pago` as p
                    JOIN contrato as c ON p.id_contrato=c.id_contrato
                    JOIN inquilino as i on c.id_inquilino=i.id_inquilino
                    JOIN inmueble as m on c.id_inmueble=m.id_inmueble
                    WHERE `estado`!= 'anulado'
                    LIMIT {(paginaNro - 1) * tamPagina}, {tamPagina};";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                var p = new PagoDTO();
                p.Id_pago = lector.GetInt16("id_pago");
                p.Id_contrato = lector.GetInt16("id_contrato");
                p.Monto = lector.GetDouble("monto");
                p.Fecha = lector.GetDateTime("fecha");
                p.Observacion = lector.IsDBNull(lector.GetOrdinal("observacion")) ? null : lector.GetString("observacion");
                p.Estado = lector.IsDBNull(lector.GetOrdinal("estado")) ? null : lector.GetString("estado");
                p.Id_inquilino = lector.GetInt16("id_inquilino");
                p.Apellido = lector.GetString("apellido");
                p.Id_inmueble = lector.GetInt16("id_inmueble");
                p.Tipo = lector.GetString("tipo");
                pagos.Add(p);
            }
            this.cerrarConexion();

            return pagos;
        }



        //solo trae id y observacion
        public List<PagoModel> IdPagosXFechaFin(string fecha, int idContrato)
        {
            var pagos = new List<PagoModel>();

            this.abrirConexion();
            string sql =
                @"SELECT p.id_pago,p.observacion FROM `pago` as p WHERE `id_contrato`=@id and `fecha`>@fecha";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", idContrato);
            comando.Parameters.AddWithValue("@fecha", fecha);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                var p = new PagoModel();
                p.Id_pago = lector.GetInt16("id_pago");
                p.Observacion = lector.GetString("observacion");
                pagos.Add(p);
            }
            this.cerrarConexion();


            return pagos;
        }

        public int TraerCantidad()
        {
            this.abrirConexion();
            string sql = @"SELECT COUNT(`id_pago`) AS cantidad FROM `pago` WHERE `estado`!='anulado'";
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


        public List<PagoModel> PagosXContrato(int idContrato)
        {
            var pagos = new List<PagoModel>();

            this.abrirConexion();
            string sql = @"SELECT * FROM `pago` WHERE `id_contrato`=@id
                        ORDER BY fecha";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", idContrato);
            comando.CommandType = CommandType.Text;
            var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                PagoModel p = new PagoModel();
                p.Id_pago = lector.GetInt16("id_pago");
                p.Id_contrato = lector.GetInt16("id_contrato");
                p.Monto = lector.GetDouble("monto");
                p.Fecha = lector.GetDateTime("fecha");
                p.Observacion = lector.IsDBNull(lector.GetOrdinal("observacion")) ? null : lector.GetString("observacion");
                p.Estado = lector.IsDBNull(lector.GetOrdinal("estado")) ? null : lector.GetString("estado");
                pagos.Add(p);
            }
            this.cerrarConexion();

            return pagos;
        }
    }
}