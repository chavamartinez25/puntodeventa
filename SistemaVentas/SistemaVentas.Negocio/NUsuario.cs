using System.Data;
using SistemaVentas.Datos;
using SistemaVentas.Entidades;

namespace SistemaVentas.Negocio
{
    public class NUsuario
    {
        public static DataTable Listar()
        {
            DUsuario Datos = new DUsuario();
            return Datos.Listar();
        }

        public static DataTable Buscar(string Valor)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Buscar(Valor);
        }

        public static DataTable LogIn(string Email, string Clave)
        {
            DUsuario Datos = new DUsuario();
            return Datos.LogIn(Email,Clave);
        }

        public static string Insertar(int IdRol,string Nombre,string TipoDocumento, string NumDocumento, string Direccion, string Telefono, string Email, string Clave)
        {
            // Creamos el ebjeto de tipo Categoria y despues lo retornamos a través del método de Datos.
            DUsuario Datos = new DUsuario();

            // Vamos a verificar si la categoria que intentamos insertar ya existe o no en el registro de la
            // Base de datos
            string Existe = Datos.Existe(Email);

            // Si la categoria ya existe, retornará el siguiente mensaje
            if (Existe.Equals("1"))
            {
                return "El usuario con ese email ya existe.";
            }
            // Si la categoria no existe, retornará insertará dicha categoría en la base de datos
            else
            {
                Usuario Obj = new Usuario();
                Obj.IdRol = IdRol;
                Obj.Nombre = Nombre;
                Obj.TipoDocumento = TipoDocumento;
                Obj.NumDocumento = NumDocumento;
                Obj.Direccion = Direccion;
                Obj.Telefono = Telefono;
                Obj.Email = Email;
                Obj.Clave = Clave;
                return Datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int Id, int IdRol, string Nombre, string TipoDocumento, string NumDocumento, string Direccion, string Telefono,string EmailAnt,string Email, string Clave)
        {
            DUsuario Datos = new DUsuario();
            Usuario Obj = new Usuario();

            if (EmailAnt.Equals(Email)) // Revisa si el nombre es el mismo, significa que no cambiará la categoria
            {
                Obj.IdUsuario = Id;
                Obj.IdRol = IdRol;
                Obj.Nombre = Nombre;
                Obj.TipoDocumento = TipoDocumento;
                Obj.NumDocumento = NumDocumento;
                Obj.Direccion = Direccion;
                Obj.Telefono = Telefono;
                Obj.Email = Email;
                Obj.Clave = Clave;
                return Datos.Actualizar(Obj);
            }
            else
            {
                string Existe = Datos.Existe(Email);
                if (Existe.Equals("1")) // Si el nombre es diferente, entonces hay que validar que esa categoria no exista
                {
                    return "El usuario con ese email ya existe.";
                }
                // Si la categoria no existe, retornará insertará dicha categoría en la base de datos
                else
                {
                    Obj.IdUsuario = Id;
                    Obj.IdRol = IdRol;
                    Obj.Nombre = Nombre;
                    Obj.TipoDocumento = TipoDocumento;
                    Obj.NumDocumento = NumDocumento;
                    Obj.Direccion = Direccion;
                    Obj.Telefono = Telefono;
                    Obj.Email = Email;
                    Obj.Clave = Clave;
                    return Datos.Actualizar(Obj);
                }
            }
        }

        public static string Eliminar(int Id)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Eliminar(Id);
        }

        public static string Activar(int Id)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Activar(Id);
        }

        public static string Desactivar(int Id)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Desactivar(Id);
        }

    }
}
