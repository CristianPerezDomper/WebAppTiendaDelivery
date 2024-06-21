using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Dominio.Entidades;

namespace Infraestructura.Data.SqlServer
{
    public class Pais_DAL
    {
        Conexion_DAL cn = new Conexion_DAL();

        public List<Pais> listado()
        {
            List<Pais> lista = new List<Pais>();
            SqlCommand cmd = new SqlCommand("select * from tb_paises", cn.getCNX);
            cn.getCNX.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Pais reg = new Pais();
                reg.Idpais = dr.GetString(0);
                reg.Nombrepais = dr.GetString(1);
                lista.Add(reg);
            }

            dr.Close();
            cn.getCNX.Close();

            return lista;
        }
    }
}
