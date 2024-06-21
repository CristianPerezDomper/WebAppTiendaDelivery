using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public class Pais
    {
        private string idpais;
        private string nombrepais;

        [DisplayName("Código")]
        public string Idpais { get => idpais; set => idpais = value; }
        [DisplayName("País")]
        public string Nombrepais { get => nombrepais; set => nombrepais = value; }
    }
}
