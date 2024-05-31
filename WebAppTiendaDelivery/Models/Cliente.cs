using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppTiendaDelivery.Models
{
    public class Cliente
    {
        private string codigoCliente;
        private string nombreCliente;
        private string direccion;
        private string descripcionPais;
        private string telefono;

        public string CodigoCliente { get => codigoCliente; set => codigoCliente = value; }
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string DescripcionPais { get => descripcionPais; set => descripcionPais = value; }
        public string Telefono { get => telefono; set => telefono = value; }
    }
}