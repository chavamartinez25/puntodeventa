using System;
using System.Data;
using SistemaVentas.Datos;
using SistemaVentas.Entidades;

namespace SistemaVentas.Negocio
{
    public class NPersona
    {

        public static DataTable Listar()
        {
            DPersona Datos = new DPersona();
            return Datos.Listar();
        }

        public static DataTable ListarProveedores()
        {
            DPersona Datos = new DPersona();
            return Datos.ListarProveedores();
        }

        public static DataTable ListarClientes()
        {
            DPersona Datos = new DPersona();
            return Datos.ListarClientes();
        }

        public static DataTable Buscar(string Valor)
        {
            DPersona Datos = new DPersona();
            return Datos.Buscar(Valor);
        }

        public static DataTable BuscarProveedores(string Valor)
        {
            DPersona Datos = new DPersona();
            return Datos.BuscarProveedores(Valor);
        }

        public static DataTable BuscarClientes(string Valor)
        {
            DPersona Datos = new DPersona();
            return Datos.BuscarClientes(Valor);
        }

        public static string Insertar(string TipoPersona, string Nombre, string TipoDocumento, string NumDocumento, string Direccion, string Telefono, string Email)
        {
            // Creamos el ebjeto de tipo Categoria y despues lo retornamos a través del método de Datos.
            DPersona Datos = new DPersona();

            // Vamos a verificar si la categoria que intentamos insertar ya existe o no en el registro de la
            // Base de datos
            string Existe = Datos.Existe(Nombre);

            // Si la categoria ya existe, retornará el siguiente mensaje
            if (Existe.Equals("1"))
            {
                return "La persona ya existe.";
            }
            // Si la categoria no existe, retornará insertará dicha categoría en la base de datos
            else
            {
                Persona Obj = new Persona();
                Obj.TipoPersona = TipoPersona;
                Obj.Nombre = Nombre;
                Obj.TipoDocumento = TipoDocumento;
                Obj.NumDocumento = NumDocumento;
                Obj.Direccion = Direccion;
                Obj.Telefono = Telefono;
                Obj.Email = Email;
                return Datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int Id, string TipoPersona,string NombreAnt, string Nombre, string TipoDocumento, string NumDocumento, string Direccion, string Telefono, string Email)
        {
            DPersona Datos = new DPersona();
            Persona Obj = new Persona();
            Console.WriteLine("Este es el id de la persona: "+Id);
            if (NombreAnt.Equals(Nombre)) // Revisa si el nombre es el mismo, significa que no cambiará la categoria
            {
                Obj.IdPersona = Id;
                Obj.TipoPersona = TipoPersona;
                Obj.Nombre = Nombre;
                Obj.TipoDocumento = TipoDocumento;
                Obj.NumDocumento = NumDocumento;
                Obj.Direccion = Direccion;
                Obj.Telefono = Telefono;
                Obj.Email = Email;
                return Datos.Actualizar(Obj);
            }
            else
            {
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1")) // Si el nombre es diferente, entonces hay que validar que esa categoria no exista
                {
                    return "Una persona con ese nombre ya existe.";
                }
                // Si la categoria no existe, retornará insertará dicha categoría en la base de datos
                else
                {
                    Obj.IdPersona = Id;
                    Obj.TipoPersona = TipoPersona;
                    Obj.Nombre = Nombre;
                    Obj.TipoDocumento = TipoDocumento;
                    Obj.NumDocumento = NumDocumento;
                    Obj.Direccion = Direccion;
                    Obj.Telefono = Telefono;
                    Obj.Email = Email;
                    return Datos.Actualizar(Obj);
                }
            }
        }

        public static string Eliminar(int Id)
        {
            DPersona Datos = new DPersona();
            return Datos.Eliminar(Id);  
        }

    }
}
