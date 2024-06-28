using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace Infraestructura.Data.SqlServer
{
    public class Conexion_DAL
    {
        SqlConnection cn = new SqlConnection("server=.;DataBase=JunioNegocio2023; uid=sa; pwd=");

        public SqlConnection getCNX
        { get { return cn; } }
    }
}
