using Dominio.Entidades;
using Infraestructura.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Negocio
{
    public class Producto_BL
    {
        Producto_DAL producto = new Producto_DAL();

        public List<Producto> listado()
        {
            return producto.listado();
        }

        public string Agregar(Producto reg)
        {
            return producto.Agregar(reg);
        }

        public string Actualizar(Producto reg)
        {
            return producto.Actualizar(reg);
        }

        public int Eliminar(int id)
        {
            return producto.Eliminar(id);
        }
        public int obtenerSecuenciaIdproducto()
        {
            return producto.obtenerSecuenciaIdProducto();
        }
        public Producto obtenerRegistro(int id)
        {
            return producto.obtenerRegistro(id);
        }
    }
}
