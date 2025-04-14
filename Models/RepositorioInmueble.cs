using System.Data;
using MySql.Data.MySqlClient;

namespace StiegerInmobiliaria.Models
{
    public class RepositorioInmueble : Conexion, IrepositorioInmueble
    {
        public int Alta(InmuebleModel i)
        {
            this.abrirConexion();
            int id = -1;
            string sql = @"INSERT INTO `inmueble`( `id_propietario`, `direccion`, `uso`, `tipo`, `ambientes`, `cordenadas`, `precio`, `disponible`, `imagen`) 
            VALUES (@id_propietario,@direccion,@uso,@tipo,@ambientes,@cordenadas,@precio,@disponible,@imagen)";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id_propietario", i.Id_propietario);
            comando.Parameters.AddWithValue("@direccion", i.Direccion);
            comando.Parameters.AddWithValue("@uso", i.Uso);
            comando.Parameters.AddWithValue("@tipo", i.Tipo);
            comando.Parameters.AddWithValue("@ambientes", i.Ambientes);
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
            string sql = @"UPDATE `inmueble` 
      SET `id_propietario`=`@id_propietario`,`direccion`=`@direccion`,`uso`=`@uso`,`tipo`=`@tipo`,`ambientes`=`@ambientes`,`cordenadas`=`@cordenadas`,`precio`=`@precio`,`disponible`=`@disponible`,`imagen`= `@imagen`
      WHERE `id_inmueble`='@Id_inmueble'";
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

        public List<InmuebleModel> TraerTodos()
        {
            List<InmuebleModel> inmuebles = new List<InmuebleModel>();
            this.abrirConexion();
            string sql = "SELECT * FROM `inmueble` WHERE `activo`=1";
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
                i.Ambientes = lector.IsDBNull(lector.GetOrdinal("ambientes")) ? -1 : lector.GetInt16("ambientes");
                i.Cordenadas = lector.IsDBNull(lector.GetOrdinal("cordenadas")) ? null : lector.GetString("cordenadas");
                i.Precio = lector.IsDBNull(lector.GetOrdinal("precio")) ? -1 : lector.GetDouble("precio");
                i.Disponible = lector.IsDBNull(lector.GetOrdinal("disponible")) ? null : lector.GetString("disponible");
                i.Imagen = lector.IsDBNull(lector.GetOrdinal("imagen")) ? null : lector.GetString("imagen");

                inmuebles.Add(i);
            }
            this.cerrarConexion();
            return inmuebles;
        }

        public InmuebleModel TraerId(int id)
        {
            this.abrirConexion();
            string sql = @"SELECT `id_propietario`, `direccion`, `uso`, `tipo`, `ambientes`, `cordenadas`, `precio`, `disponible`, `activo`, `imagen` 
      FROM `inmueble` WHERE `id_inmueble`=@id_inmueble";
            MySqlCommand comando = new MySqlCommand(sql, this.conexionsql);
            comando.Parameters.AddWithValue("@id", id);
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
                i.Ambientes = lector.IsDBNull(lector.GetOrdinal("ambientes")) ? -1 : lector.GetInt16("ambientes");
                i.Cordenadas = lector.IsDBNull(lector.GetOrdinal("cordenadas")) ? null : lector.GetString("cordenadas");
                i.Precio = lector.IsDBNull(lector.GetOrdinal("precio")) ? -1 : lector.GetDouble("precio");
                i.Disponible = lector.IsDBNull(lector.GetOrdinal("disponible")) ? null : lector.GetString("disponible");
                i.Imagen = lector.IsDBNull(lector.GetOrdinal("imagen")) ? null : lector.GetString("imagen");
            }
            else
            {
                i.Id_inmueble = -1;
            }
            this.cerrarConexion();
            return i;
        }
    }
}