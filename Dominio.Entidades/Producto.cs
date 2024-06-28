using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Producto
    {
        private int idProducto;
        private string nombreProducto;
        private int idProveedor;
        private int idCategoria;
        private string uMedida;
        private decimal precioUnidad;
        private int unidadesEnExistencia;

        public int IdProducto { get => idProducto; set => idProducto = value; }
        public string NombreProducto { get => nombreProducto; set => nombreProducto = value; }
        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public string UMedida { get => uMedida; set => uMedida = value; }
        public decimal PrecioUnidad { get => precioUnidad; set => precioUnidad = value; }
        public int UnidadesEnExistencia { get => unidadesEnExistencia; set => unidadesEnExistencia = value; }
    }
}
