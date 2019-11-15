using System.Data;
using SistemaVentas.Datos;
using SistemaVentas.Entidades;

namespace SistemaVentas.Negocio
{
    public class NArticulo
    {
        public static DataTable Listar()
        {
            DArticulo Datos = new DArticulo();
            return Datos.Listar();
        }

        public static DataTable Buscar(string Valor)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Buscar(Valor);
        }

        public static string Insertar(int IdCategoria, string Codigo,string Nombre, decimal PrecioVenta, int Stock, string Descripcion, string Imagen)
        {
            // Creamos el ebjeto de tipo Categoria y despues lo retornamos a través del método de Datos.
            DArticulo Datos = new DArticulo();

            // Vamos a verificar si la categoria que intentamos insertar ya existe o no en el registro de la
            // Base de datos
            string Existe = Datos.Existe(Nombre);

            // Si la categoria ya existe, retornará el siguiente mensaje
            if (Existe.Equals("1"))
            {
                return "El artículo ya existe.";
            }
            // Si la categoria no existe, retornará insertará dicha categoría en la base de datos
            else
            {
                EArticulo Obj = new EArticulo();
                Obj.IdCategoria = IdCategoria;
                Obj.Codigo = Codigo;
                Obj.Nombre = Nombre;
                Obj.PrecioVenta = PrecioVenta;
                Obj.Stock = Stock;
                Obj.Descripcion = Descripcion;
                Obj.Imagen = Imagen;
                return Datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int Id, int IdCategoria, string Codigo, string NombreAnt, string Nombre, decimal PrecioVenta, int Stock, string Descripcion, string Imagen)
        {
            DArticulo Datos = new DArticulo();
            EArticulo Obj = new EArticulo();

            if (NombreAnt.Equals(Nombre)) // Revisa si el nombre es el mismo, significa que no cambiará la categoria
            {
                Obj.IdArticulo = Id;
                Obj.IdCategoria = IdCategoria;
                Obj.Codigo = Codigo;
                Obj.Nombre = Nombre;
                Obj.PrecioVenta = PrecioVenta;
                Obj.Stock = Stock;
                Obj.Descripcion = Descripcion;
                Obj.Imagen = Imagen;
                return Datos.Actualizar(Obj);
            }
            else
            {
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1")) // Si el nombre es diferente, entonces hay que validar que esa categoria no exista
                {
                    return "El artículo ya existe.";
                }
                // Si la categoria no existe, retornará insertará dicha categoría en la base de datos
                else
                {
                    Obj.IdArticulo = Id;
                    Obj.IdCategoria = IdCategoria;
                    Obj.Codigo = Codigo;
                    Obj.Nombre = Nombre;
                    Obj.PrecioVenta = PrecioVenta;
                    Obj.Stock = Stock;
                    Obj.Descripcion = Descripcion;
                    Obj.Imagen = Imagen;
                    return Datos.Actualizar(Obj);
                }
            }
        }

        public static string Eliminar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Eliminar(Id);
        }

        public static string Activar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Activar(Id);
        }

        public static string Desactivar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Desactivar(Id);
        }
    }
}
