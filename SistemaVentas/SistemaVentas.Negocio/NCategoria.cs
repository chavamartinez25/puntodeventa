using System.Data;
using SistemaVentas.Datos;
using SistemaVentas.Entidades;

namespace SistemaVentas.Negocio
{
    public class NCategoria
    {
        // Seran estaticas ya que solo se hará referencia a las funciones, no se van a instanciar 
        public static DataTable Listar()
        {
            DCategoria Datos = new DCategoria();
            return Datos.Listar();
        }

        public static DataTable Buscar( string Valor )
        {
            DCategoria Datos = new DCategoria();
            return Datos.Buscar( Valor );
        }

        public static DataTable Seleccionar()
        {
            DCategoria Datos = new DCategoria();
            return Datos.Seleccionar();
        }

        public static string Insertar( string Nombre, string Descripcion)
        {
            // Creamos el ebjeto de tipo Categoria y despues lo retornamos a través del método de Datos.
            DCategoria Datos = new DCategoria();

            // Vamos a verificar si la categoria que intentamos insertar ya existe o no en el registro de la
            // Base de datos
            string Existe = Datos.Existe( Nombre );

            // Si la categoria ya existe, retornará el siguiente mensaje
            if (Existe.Equals("1"))
            {
                return "La categoria ya existe.";
            }
            // Si la categoria no existe, retornará insertará dicha categoría en la base de datos
            else
            {
                Categoria Obj = new Categoria();
                Obj.Nombre = Nombre;
                Obj.Descripcion = Descripcion;
                return Datos.Insertar(Obj);
            }
        }

        public static string Actualizar( int Id,string NombreAnt, string Nombre, string Descripcion)
        {
            DCategoria Datos = new DCategoria();
            Categoria Obj = new Categoria();

            if (NombreAnt.Equals(Nombre)) // Revisa si el nombre es el mismo, significa que no cambiará la categoria
            {
                Obj.IdCategoria = Id;
                Obj.Nombre = Nombre;
                Obj.Descripcion = Descripcion;
                return Datos.Actualizar(Obj);
            }
            else
            {
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1")) // Si el nombre es diferente, entonces hay que validar que esa categoria no exista
                {
                    return "La categoria ya existe.";
                }
                // Si la categoria no existe, retornará insertará dicha categoría en la base de datos
                else
                {
                    Obj.IdCategoria = Id;
                    Obj.Nombre = Nombre;
                    Obj.Descripcion = Descripcion;
                    return Datos.Actualizar(Obj);
                }
            }
        }

        public static string Eliminar( int Id )
        {
            DCategoria Datos = new DCategoria();
            return Datos.Eliminar(Id);
        }

        public static string Activar(int Id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Activar(Id);
        }

        public static string Desactivar(int Id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Desactivar(Id);
        }
    }
}
