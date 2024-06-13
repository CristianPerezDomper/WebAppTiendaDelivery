using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppTiendaDelivery.Models
{
    public class Pedido
    {
        private int idPedido;
        private DateTime fechaPedido;
        private string cliente;
        private string direccionDestinatario;
        private string ciudadDestinatario;
        private string paisDestinatario;
        private double montoTotal;
        private string empleado;

        public int IdPedido { get => idPedido; set => idPedido = value; }
        public DateTime FechaPedido { get => fechaPedido; set => fechaPedido = value; }
        public string Cliente { get => cliente; set => cliente = value; }
        public string DireccionDestinatario { get => direccionDestinatario; set => direccionDestinatario = value; }
        public string CiudadDestinatario { get => ciudadDestinatario; set => ciudadDestinatario = value; }
        public string PaisDestinatario { get => paisDestinatario; set => paisDestinatario = value; }
        public double MontoTotal { get => montoTotal; set => montoTotal = value; }
        public string Empleado { get => empleado; set => empleado = value; }
    }
}