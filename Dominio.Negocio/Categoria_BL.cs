using Infraestructura.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Dominio.Negocio
{
    public class Categoria_BL
    {
        Categoria_DAL categoria  = new Categoria_DAL();

        public List<Categoria> listado()
        {
            return categoria.listado();
        }
    }
}
