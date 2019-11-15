
namespace SistemaVentas.Entidades
{
    public class Categoria
    {
        public int IdCategoria
        {
            get; // Obtiene valores de esta categoria
            set; // Escribe valores de esta categoria
        }
        public string Nombre
        {
            get;
            set;
        }
        public string Descripcion
        {
            get;
            set;
        }
        public bool Estado
        {
            get;
            set;
        }
    }
}
