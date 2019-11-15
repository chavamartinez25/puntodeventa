using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SistemaVentas.Entidades;
using System.Data.SqlClient;

namespace SistemaVentas.Datos
{
    public class DCategoria
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
                SqlCommand Comando = new SqlCommand("categoria_listar", SqlCon);
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
                SqlCommand Comando = new SqlCommand("categoria_buscar", SqlCon);
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

        // Selecciona todas las categorias de la base de datos
        public DataTable Seleccionar()
        {
            SqlDataReader Resultado; // Lectura de filas
            DataTable Tabla = new DataTable(); // Representa una tabla en memoria
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion(); // Devolverá la variable crear conexion
                // SqlCommand ejecuta un proceso almacenado en la base de datos
                SqlCommand Comando = new SqlCommand("categoria_seleccionar", SqlCon);
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

        public string Existe(string Valor)
        {
            string Respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_existe", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = Valor;
                SqlParameter ParExiste = new SqlParameter(); // Retornará el parametro de respuesta de la base de datos
                ParExiste.ParameterName = "@existe"; // Nombre del parametro a retornar 
                ParExiste.SqlDbType = SqlDbType.Int; // Tipo de dato del parametro a retornar
                ParExiste.Direction = ParameterDirection.Output; // Indicamos que el parametro es de salida
                Comando.Parameters.Add(ParExiste); // Agregamos el parámetro a la llamada al Stored Procedure
                SqlCon.Open();
                Comando.ExecuteNonQuery(); // Ejeutamos el Stored Procedure
                Respuesta = Convert.ToString(ParExiste.Value); // Convertimos a String
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

        // Esta funcion espera de entrada un objeto de la capa Entidades donde se definio el modelo
        // de Categoria
        public string Insertar(Categoria obj)
        {
            string Respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_insertar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@nombre",SqlDbType.VarChar).Value = obj.Nombre;
                Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = obj.Descripcion;
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

        public string Actualizar(Categoria obj)
        {
            string Respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_actualizar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = obj.IdCategoria;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = obj.Nombre;
                Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = obj.Descripcion;  
                SqlCon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo actualizar el registro";
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
        // Solo desactiva, por lo que recibe un entero
        public string Eliminar(int Id)
        {
            string Respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_eliminar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = Id;
                SqlCon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo eliminar el registro";
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

        public string Activar(int Id)
        {
            string Respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_activar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = Id;
                SqlCon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo activar el registro";
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

        public string Desactivar(int Id)
        {
            string Respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_desactivar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = Id;
                SqlCon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo eliminar el registro";
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
