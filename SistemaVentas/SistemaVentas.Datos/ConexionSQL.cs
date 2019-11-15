using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SistemaVentas.Datos
{
    public class ConexionSQL
    {
        // Estas variables nos permitirán crear la cadena de conexión con la BD
        private string Base;
        private string Servidor;
        private string Usuario;
        private string prueba;
        private string Clave;
        private bool Seguridad;
        // Este método es estatico e instancia a la misma clase para compartir la cadena de conexión
        // con la base de datos en SQL.
        private static ConexionSQL Con = null;

        // Este será el constructor y es privado para no poder ser instanciado desde otra clase
        // mas que por esta misma clase.
        private ConexionSQL()
        {
            this.Base = "dbsistema";
            this.Servidor = "(localdb)\\LocalServer";
            this.Usuario = "";
            this.Clave = "";
            this.Seguridad = true;
        }

        public SqlConnection CrearConexion()
        {
            SqlConnection Cadena = new SqlConnection();

            try
            {
                // Cadena de conexión sin pwd ni usr dependiendo del método de seguridad.
                Cadena.ConnectionString = "Server=" + this.Servidor + "; Database=" + this.Base + ";" ;
                
                // Si la seguridad de la base de datos es de Windows utilizará la siguiente configuración
                if (this.Seguridad == true)
                {
                    // Agregamos la conexión segura de Windows
                    Cadena.ConnectionString = Cadena.ConnectionString + "Integrated Security=SSPI";
                } else // Si la seguridad esta a cargo de SQL utilizará la siguiente configuración: 
                {
                    Cadena.ConnectionString = Cadena.ConnectionString + "User Id=" + this.Usuario + 
                                              "; Passwords=" + this.Clave ;
                }
            }
            catch (Exception ex)
            {
                Cadena = null; // Si da error asignará null a la cadena de conexion
                throw ex;
            }
            // Retorna cadena de conexión
            return Cadena;
        }

        // Se revisa si ya se tiene una instancia de esta clase
        public static ConexionSQL getInstancia()
        {
            // Si no hay instancia, se crea una nueva instancia
            if( Con == null)
            {
                Con = new ConexionSQL();
            }
            return Con;
        }

    }
}
