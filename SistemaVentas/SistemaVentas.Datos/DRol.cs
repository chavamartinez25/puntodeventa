using System;
using System.Data;
using System.Data.SqlClient;


namespace SistemaVentas.Datos
{
    public class DRol
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
                SqlCommand Comando = new SqlCommand("rol_listar", SqlCon);
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

    }
}
