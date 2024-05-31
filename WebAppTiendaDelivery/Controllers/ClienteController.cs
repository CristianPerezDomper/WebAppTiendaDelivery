using System;
using WebAppTiendaDelivery.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace WebAppTiendaDelivery.Controllers
{
    public class ClienteController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ToString());

        int numreg = 10;

        // GET: Cliente
        
        List<Cliente> obtenerClientes()
        {
            //Crea listOf del tipo Cliente
            List<Cliente> lstClientes = new List<Cliente>();

            //Definimos un SqlCommand y su CommandType
            SqlCommand cmd = new SqlCommand("usp_listaCliente",cn);
            cmd.CommandType = CommandType.StoredProcedure;

            //Abrimos conexión
            cn.Open();

            //Ejecutamos el SqlCommand
            SqlDataReader dr = cmd.ExecuteReader();

            //Recuperamos los valores del SqlDataReader
            while(dr.Read())
            {
                Cliente reg = new Cliente();

                reg.CodigoCliente = dr["IdCliente"].ToString();
                reg.NombreCliente = dr["NombreCia"].ToString();
                reg.Direccion = dr["Direccion"].ToString();
                reg.DescripcionPais = dr["NombrePais"].ToString();
                reg.Telefono = dr["Telefono"].ToString();

                lstClientes.Add(reg);
            }

            //Cerramos el SqlDataReader y la conexión a la BD
            dr.Close();
            cn.Close();

            return lstClientes;
        }



        public ActionResult ListaClientes(int? pag = null)
        {
            int c = obtenerClientes().Count;
            ViewBag.numreg = c %numreg != 0 ? c / numreg +1 : c / numreg;

            int pageact = pag == null ? 0 : (int)pag;
            int reginicio = pageact * numreg;
            int regfin = reginicio + numreg;

            List<Cliente> lista = new List<Cliente>();
            for (int i = reginicio; i < regfin; i++)
            {
                if (i == c) break;
                lista.Add(obtenerClientes()[i]);
            }
            return View(lista);
        }
    }
}