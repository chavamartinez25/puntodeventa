using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SistemaVentas.Datos;

namespace SistemaVentas.Negocio
{
    public class NRol
    {

        public static DataTable Listar()
        {
            DRol Datos = new DRol();
            return Datos.Listar(); // Retornamos el resultado de la funcion Listar de la clase Datos de Rol.
        }

    }
}
