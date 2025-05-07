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
                @"INSERT INTO `pago`(`id_contrato`, `monto`, `fecha`, `observacion`, `estado`)
                VALUES (@idContrato,@monto,@fecha,@observacion,@estado);
               SELECT LAST_INSERT_ID();";

            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);

            comando.Parameters.AddWithValue("@idContrato", p.Id_contrato);
            comando.Parameters.AddWithValue("@monto", p.Monto);
            comando.Parameters.AddWithValue("@fecha", p.Fecha);
            comando.Parameters.AddWithValue("@observacion", p.Observacion);
            comando.Parameters.AddWithValue("@estado", p.Estado);
            comando.CommandType = CommandType.Text;
            id = Convert.ToInt32(comando.ExecuteScalar());
            p.Id_pago = id;
            this.cerrarConexion();

            return id;
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

        public List<PagoModel> TraerTodos()
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

/*
,i.id_inquilino,i.apellido,m.id_inmueble ,m.tipo
                    JOIN inquilino as i on c.id_inquilino=i.id_inquilino
                    JOIN inmueble as m on c.id_inmueble=m.id_inmueble
*/
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

        public List<PagoDTO> TraerTodosDTO()
        {
            var pagos = new List<PagoDTO>();

            this.abrirConexion();
            string sql =
                @"SELECT `id_pago`,c.id_contrato,p.monto,`fecha`,`observacion`,`estado`,i.id_inquilino,i.apellido,m.id_inmueble ,m.tipo
                    FROM `pago` as p
                    JOIN contrato as c ON p.id_contrato=c.id_contrato
                    JOIN inquilino as i on c.id_inquilino=i.id_inquilino
                    JOIN inmueble as m on c.id_inmueble=m.id_inmueble";
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



    }
}