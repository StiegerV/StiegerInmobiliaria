using MySql.Data.MySqlClient;
using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Models
{
    public class RepositorioContrato : Conexion, iRepositorioContrato
    {

        //una vez implementado sistema de usuarios cambiar el harcodeo de creado por
        public int Alta(ContratoModel c)
        {
            this.abrirConexion();
            int id = -1;
            string sql = @"INSERT INTO contrato(monto, fecha_inicio, fecha_fin, id_inmueble, id_inquilino, fecha_fin_original, creado_por) 
               VALUES (@monto, @fecha_inicio, @fecha_fin, @id_inmueble, @id_inquilino, @fecha_fin_original, 1);
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
            id = Convert.ToInt32(comando.ExecuteScalar());
            c.Id_contrato = id;
            this.cerrarConexion();

            return id;
        }

        /**/
        ////una vez implementado sistema de usuarios cambiar el harcodeo de terminado por
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


        /*
        checkear que:
        el inmueble no este ocupado en las nuevas fecha
        el inquilino no tenga otro contrato activo
        */
        public int Modificacion(ContratoModel c)
        {
            this.abrirConexion();
            int columnasAfectadas = -1;
            string sql = @"*UPDATE `contrato` 
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

        public List<ContratoModel> TraerTodos()
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
    }
}