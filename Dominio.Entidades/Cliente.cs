using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public class Cliente
    {
        private string idcliente;
        private string nombreCia;
        private string direccion;
        private string idpais;
        private string telefono;

        [DisplayName("Código cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese el código")]
        public string Idcliente { get => idcliente; set => idcliente = value; }
        [DisplayName("Nombre del cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese el nombre")]
        public string NombreCia { get => nombreCia; set => nombreCia = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        [DisplayName("País")]
        public string Idpais { get => idpais; set => idpais = value; }
        public string Telefono { get => telefono; set => telefono = value; }
    }
}
