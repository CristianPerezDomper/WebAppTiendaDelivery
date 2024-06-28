using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.SqlServer
{
    public class Proveedor_DAL
    {
        Conexion_DAL cn = new Conexion_DAL();

        public List<Proveedor> listado()
        {
            //Creamos ListOf del tipo Proveedor 
            List<Proveedor> lista = new List<Proveedor>();
            SqlCommand cmd = new SqlCommand("spGetAllProvider", cn.getCNX);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //Abrimos conexión
            cn.getCNX.Open();

            //Ejecutamos el SqlCommand
            SqlDataReader dr= cmd.ExecuteReader();

            while (dr.Read()) 
            {
                Proveedor reg = new Proveedor();
                reg.IdProveedor = dr.GetInt32(0);
                reg.NombreCia = dr.GetString(1);
                lista.Add(reg);
            }

            dr.Close();
            cn.getCNX.Close();

            return lista;
        }
    }
}
