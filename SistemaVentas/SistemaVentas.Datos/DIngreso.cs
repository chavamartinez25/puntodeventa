using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SistemaVentas.Entidades;

namespace SistemaVentas.Datos
{
    public class DIngreso
    {
        public DataTable Listar()
        {
            SqlDataReader Resultado; // Lectura de filas
            DataTable Tabla = new DataTable(); // Representa una tabla en memoria
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion(); // Devolverá la variable crear conexion
                // SqlCommand ejecuta un proceso almacenado en la base de datos
                SqlCommand Comando = new SqlCommand("ingreso_listar", SqlCon);
                // Indicamos que estamos haciendo referencia a un proceso almacenado a la base de datos
                Comando.CommandType = CommandType.StoredProcedure;
                SqlCon.Open(); // Se abre conexion
                Resultado = Comando.ExecuteReader(); // Se ejecuta y gaurdamos el resultado en la variable
                Tabla.Load(Resultado); // Carga los datos en la variable Tabla
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Si la conexion está abierta, entonces se cierra la misma.
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }
        public DataTable Buscar(string valor)
        {
            SqlDataReader Resultado; // Lectura de filas
            DataTable Tabla = new DataTable(); // Representa una tabla en memoria
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion(); // Devolverá la variable crear conexion
                // SqlCommand ejecuta un proceso almacenado en la base de datos
                SqlCommand Comando = new SqlCommand("ingreso_buscar", SqlCon);
                // Indicamos que estamos haciendo referencia a un proceso almacenado a la base de datos
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor; // Indicamos el valor que se enviará para la busqueda que insertamos en la llamada a la funcion
                SqlCon.Open(); // Se abre conexion
                Resultado = Comando.ExecuteReader(); // Se ejecuta y gaurdamos el resultado en la variable
                Tabla.Load(Resultado); // Carga los datos en la variable Tabla
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Si la conexion está abierta, entonces se cierra la misma.
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }
        public string Insertar(Ingreso obj)
        {
            string Respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("ingreso_insertar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idproveedor", SqlDbType.Int).Value = obj.IdProveedor;
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = obj.IdUsuario;
                Comando.Parameters.Add("@tipo_comprobante", SqlDbType.Int).Value = obj.TipoComprobante;
                Comando.Parameters.Add("@serie_comprobante", SqlDbType.Int).Value = obj.SerieComprobante;
                Comando.Parameters.Add("@num_comprobante", SqlDbType.Int).Value = obj.NumComprobante;
                Comando.Parameters.Add("@impuesto", SqlDbType.Decimal).Value = obj.Impuesto;
                Comando.Parameters.Add("@total", SqlDbType.Decimal).Value = obj.Total;
                Comando.Parameters.Add("@detalle", SqlDbType.Structured).Value = obj.Detalles;
                SqlCon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo ingresar el registro";
            }
            catch (Exception ex)
            {
                Respuesta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Respuesta;
        }
        public string Anular(int Id)
        {
            string Respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("ingreso_anular", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idingreso", SqlDbType.Int).Value = Id;
                SqlCon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo anular el registro";
            }
            catch (Exception ex)
            {
                Respuesta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Respuesta;
        }
    }
}
