using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Proveedor
    {
        private int idProveedor;
        private string nombreCia;
        private string nombreContacto;
        private string cargoContacto;
        private string direccion;
        private string idpais;
        private string telefono;
        private string fax;

        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
        public string NombreCia { get => nombreCia; set => nombreCia = value; }
        public string NombreContacto { get => nombreContacto; set => nombreContacto = value; }
        public string CargoContacto { get => cargoContacto; set => cargoContacto = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Idpais { get => idpais; set => idpais = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Fax { get => fax; set => fax = value; }
    }
}
