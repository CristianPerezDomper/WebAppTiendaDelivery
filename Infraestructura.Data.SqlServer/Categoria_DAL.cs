using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dominio.Entidades;
using System.Data.SqlClient;

namespace Infraestructura.Data.SqlServer
{
    public class Categoria_DAL
    {
        Conexion_DAL cn = new Conexion_DAL();

        public List<Categoria> listado()
        {
            //Creamos Listof del tipo categoria
            List<Categoria> lista = new List<Categoria>();
            SqlCommand cmd = new SqlCommand("spGetAllCategory", cn.getCNX);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //Abrimos conexión
            cn.getCNX.Open();

            //Ejecutamos el SqlCommand
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read()) 

            { 
                Categoria reg = new Categoria();
                reg.IdCategoria = dr.GetInt32(0);
                reg.NombreCategoria = dr.GetString(1);
                lista.Add(reg);

            }

            dr.Close();
            cn.getCNX.Close();

            return lista;

        }
    }
}
