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
            SqlCommand cmd = new SqlCommand("spGetAllClient", cn);
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

        //Definimos el método
        List<Pedido> obtenerPedidoPorFechas(string fecInicio , string fecFin)
        {
            //Creamos un listof del tipo pedido

            List<Pedido> lstPedidos = new List<Pedido>();

            //Definimos un SqlCommand y su CommandType
            SqlCommand cmd = new SqlCommand("spGetOrderByDate",cn);
            cmd.CommandType = CommandType.StoredProcedure;

            //Añadimos parámetros --> validamos si los parámetros son nulos o vacíos

            if(!(string.IsNullOrEmpty(fecInicio) && string.IsNullOrEmpty(fecFin)))
            {
                cmd.Parameters.AddWithValue("@finicio", Convert.ToDateTime(fecInicio));
                cmd.Parameters.AddWithValue("@ffin", Convert.ToDateTime(fecFin));
            }
            else
            {
                cmd.Parameters.AddWithValue("@finicio", DBNull.Value);
                cmd.Parameters.AddWithValue("@ffin", DBNull.Value);
            }

            //Abrimos conexión
            cn.Open();

            //Ejecutamos el SqlCommand
            SqlDataReader dr = cmd.ExecuteReader();

            //Recuperamos los valores del SqlDataReader
            while (dr.Read()) 
            { 
                Pedido reg = new Pedido();

                reg.IdPedido = Convert.ToInt32(dr["IdPedido"].ToString());
                reg.FechaPedido = Convert.ToDateTime(dr["FechaPedido"].ToString());
                reg.Cliente = dr["Cliente"].ToString();
                reg.DireccionDestinatario = dr["DireccionDestinatario"].ToString();
                reg.CiudadDestinatario = dr["CiudadDestinatario"].ToString();
                reg.PaisDestinatario = dr["PaisDestinatario"].ToString();
                reg.MontoTotal = Convert.ToDouble(dr["MontoTotal"].ToString());
                reg.Empleado = dr["Empleado"].ToString();

                lstPedidos.Add(reg);
            }

            //Cerramos el SqlDataReader y la conexión a la BD
            dr.Close();
            cn.Close();

            return lstPedidos;
        }
        public ActionResult PedidosFechas(string fecInicio , string fecFin, int ? pag = null)
        {
            //Obtenemos la lista de pedidos segun el rango de fechas
            List<Pedido> listaPedidos = obtenerPedidoPorFechas(fecInicio,fecFin);

            //Recupero la cantidad de registros
            int c = listaPedidos.Count;

            //Almaceno en el ViewBag una propiedad numPagina
            // numPagina = determina la cantidad de páginas
            ViewBag.numPagina = c % numreg !=0 ? c/ numreg + 1 : c / numreg;

            //Identificaos el número de página actual
            int pageact = pag == null ? 0 : (int)pag;

            //Calculamos el indice de registro de inicio y de fin
            int reginicio = pageact * numreg;
            int regfin = reginicio + numreg;

            //Creamos una lista temporal del tipo List<Pedido>
            List<Pedido> listaFiltrada = new List<Pedido>();

            //Recorremos un for en base a los indices de registro calculado

            for(int i = reginicio; i < regfin; i++)
            {

                //Si el contador i es igual a c entocnes sale del for
                if (i == c)
                    break;

                //Aquí obtenemos el objeto cliente de la posición i
                listaFiltrada.Add(listaPedidos[i]);
            }

            //Enviamos datos a la vista 
            ViewBag.fecInicio = fecInicio;
            ViewBag.fecFin = fecFin;
            return View(listaFiltrada);
        }

        List<Pedido> obtenerPedidoPorCliente(string cliente)
        {
            //Creamos un ListOf del tipo pedido
            List<Pedido> lstPedidos = new List<Pedido>();

            //Definimos un SQLCommand y su CommandType
            SqlCommand cmd = new SqlCommand("spGetOrderByCustomer", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            //Añadimos parámetros --> validamos si los parámetros son nulos o vacíos
            if (!(string.IsNullOrEmpty(cliente)))
            {
                cmd.Parameters.AddWithValue("@idCliente", cliente);
            }
            else
            {
                cmd.Parameters.AddWithValue("@idCliente", DBNull.Value);
            }

            //Abrimos la conexión
            cn.Open();

            //Ejecutamos el SQLCommand
            SqlDataReader dr = cmd.ExecuteReader();

            //Recuperamos los valores del SqlDataReader

            while (dr.Read())
            {
                Pedido reg = new Pedido();

                reg.IdPedido = Convert.ToInt32(dr["IdPedido"].ToString());
                reg.FechaPedido = Convert.ToDateTime(dr["FechaPedido"].ToString());
                reg.Cliente = dr["Cliente"].ToString();
                reg.DireccionDestinatario = dr["DireccionDestinatario"].ToString();
                reg.CiudadDestinatario = dr["CiudadDestinatario"].ToString();
                reg.PaisDestinatario = dr["PaisDestinatario"].ToString();
                reg.MontoTotal = Convert.ToDouble(dr["MontoTotal"].ToString());
                reg.Empleado = dr["Empleado"].ToString();

                lstPedidos.Add(reg);
            }

            //Cerramos el SqlDataReader y la conexión a la BD
            dr.Close();
            cn.Close();

            return lstPedidos;
        }

        public ActionResult PedidosCliente(string idCliente,  int? pag = null)
        {
            //Obtenemos la lista de pedidos segun el rango de fechas
            List<Pedido> listaPedidos = obtenerPedidoPorCliente(idCliente);

            //Recupero la cantidad de registros
            int c = listaPedidos.Count;

            //Almaceno en el ViewBag una propiedad numPagina
            // numPagina = determina la cantidad de páginas
            ViewBag.numPagina = c % numreg != 0 ? c / numreg + 1 : c / numreg;

            //Identificaos el número de página actual
            int pageact = pag == null ? 0 : (int)pag;

            //Calculamos el indice de registro de inicio y de fin
            int reginicio = pageact * numreg;
            int regfin = reginicio + numreg;

            //Creamos una lista temporal del tipo List<Pedido>
            List<Pedido> listaFiltrada = new List<Pedido>();

            //Recorremos un for en base a los indices de registro calculado

            for (int i = reginicio; i < regfin; i++)
            {

                //Si el contador i es igual a c entocnes sale del for
                if (i == c)
                    break;

                //Aquí obtenemos el objeto cliente de la posición i
                listaFiltrada.Add(listaPedidos[i]);
            }

            //Enviamos datos a la vista 
            ViewBag.idCliente = idCliente;

            //ViewBag que almacena un SelectList de tb_clientes | tener en cuenta los 2 atributos de la clase
            ViewBag.listaClientes = new SelectList(obtenerClientes(),"CodigoCliente","NombreCliente",idCliente);


            return View(listaFiltrada);
        }
    }
}