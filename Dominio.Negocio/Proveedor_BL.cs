using Dominio.Entidades;
using Infraestructura.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Negocio
{
    public class Proveedor_BL
    {
        Proveedor_DAL proveedor = new Proveedor_DAL();

        public List <Proveedor> listado()
        {
            return proveedor.listado();
        }
    }
}
