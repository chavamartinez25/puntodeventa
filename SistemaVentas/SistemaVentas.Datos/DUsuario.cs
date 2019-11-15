using System;
using System.Data;
using System.Data.SqlClient;
using SistemaVentas.Entidades;

namespace SistemaVentas.Datos
{
    public class DUsuario
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
                SqlCommand Comando = new SqlCommand("usuario_listar", SqlCon);
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
                SqlCommand Comando = new SqlCommand("usuario_buscar", SqlCon);
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

        public string Existe(string Valor)
        {
            string Respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_existe", SqlCon);
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
        public string Insertar(Usuario obj)
        {
            string Respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_insertar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idrol", SqlDbType.Int).Value = obj.IdRol;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = obj.Nombre;
                Comando.Parameters.Add("@tipo_documento", SqlDbType.VarChar).Value = obj.TipoDocumento;
                Comando.Parameters.Add("@num_documento", SqlDbType.VarChar).Value = obj.NumDocumento;
                Comando.Parameters.Add("@direccion", SqlDbType.VarChar).Value = obj.Direccion;
                Comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = obj.Telefono;
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                Comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = obj.Clave;
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

        public string Actualizar(Usuario obj)
        {
            string Respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_actualizar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = obj.IdUsuario;
                Comando.Parameters.Add("@idrol", SqlDbType.Int).Value = obj.IdRol;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = obj.Nombre;
                Comando.Parameters.Add("@tipo_documento", SqlDbType.VarChar).Value = obj.TipoDocumento;
                Comando.Parameters.Add("@num_documento", SqlDbType.VarChar).Value = obj.NumDocumento;
                Comando.Parameters.Add("@direccion", SqlDbType.VarChar).Value = obj.Direccion;
                Comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = obj.Telefono;
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                Comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = obj.Clave;
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
                SqlCommand Comando = new SqlCommand("usuario_eliminar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = Id;
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
                SqlCommand Comando = new SqlCommand("usuario_activar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = Id;
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
                SqlCommand Comando = new SqlCommand("usuario_desactivar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = Id;
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

        public DataTable LogIn(string Email, string Clave)
        {
            SqlDataReader Resultado; // Lectura de filas
            DataTable Tabla = new DataTable(); // Representa una tabla en memoria
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = ConexionSQL.getInstancia().CrearConexion(); // Devolverá la variable crear conexion
                // SqlCommand ejecuta un proceso almacenado en la base de datos
                SqlCommand Comando = new SqlCommand("usuario_login", SqlCon);
                // Indicamos que estamos haciendo referencia a un proceso almacenado a la base de datos
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = Email; // Indicamos el valor que se enviará para la busqueda que insertamos en la llamada a la funcion
                Comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = Clave; // Indicamos el valor que se enviará para la busqueda que insertamos en la llamada a la funcion
                SqlCon.Open(); // Se abre conexion
                Resultado = Comando.ExecuteReader(); // Se ejecuta y gaurdamos el resultado en la variable
                Tabla.Load(Resultado); // Carga los datos en la variable Tabla
                return Tabla;
            }
            catch (Exception ex)
            {
                return null; // Si tenemos algun error, retornará un null
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
