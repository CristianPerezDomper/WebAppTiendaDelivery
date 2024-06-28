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
    public class Cliente_DAL
    {
        Conexion_DAL cn = new Conexion_DAL();

        public List<Cliente> listado()
        {
            List<Cliente> lista = new List<Cliente>();
            SqlCommand cmd = new SqlCommand("select * from tb_clientes", cn.getCNX);
            cn.getCNX.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                Cliente reg = new Cliente();
                reg.Idcliente = dr.GetString(0);
                reg.NombreCia = dr.GetString(1);
                reg.Direccion = dr.GetString(2);
                reg.Idpais = dr.GetString(3);
                reg.Telefono = dr.GetString(4);
                lista.Add(reg);
            }

            dr.Close();
            cn.getCNX.Close();

            return lista;
        }

        public string Agregar(Cliente reg)
        {
            string msg = "";
            cn.getCNX.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("insert tb_clientes values (@id,@nom,@dir,@pais,@fono)", cn.getCNX);
                cmd.Parameters.AddWithValue("@id", reg.Idcliente);
                cmd.Parameters.AddWithValue("@nom", reg.NombreCia);
                cmd.Parameters.AddWithValue("@dir", reg.Direccion);
                cmd.Parameters.AddWithValue("@pais", reg.Idpais);
                cmd.Parameters.AddWithValue("@fono", reg.Telefono);

                int cantReg = cmd.ExecuteNonQuery();
                msg = cantReg.ToString() + " registro(s) agregado(s)";

            }
            catch (SqlException ex) { 
                msg = ex.Message.ToString();
            } finally { cn.getCNX.Close(); }

            return msg;
        }

        public string Actualizar(Cliente reg) 
        {
            string msg = "";
            cn.getCNX.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("update tb_clientes set nombrecia=@nom, direccion=@dir, idpais=@pais, telefono=@fono where idCliente=@id", cn.getCNX);
                cmd.Parameters.AddWithValue("@id", reg.Idcliente);
                cmd.Parameters.AddWithValue("@nom", reg.NombreCia);
                cmd.Parameters.AddWithValue("@dir", reg.Direccion);
                cmd.Parameters.AddWithValue("@pais", reg.Idpais);
                cmd.Parameters.AddWithValue("@fono", reg.Telefono);

                int cantReg = cmd.ExecuteNonQuery();
                msg = cantReg.ToString() + " registro(s) actualizado(s)";

            }
            catch (SqlException ex)
            {
                msg = ex.Message.ToString();
            }
            finally { cn.getCNX.Close(); }

            return msg;

        }

        public string Eliminar(string id)
        {
            string msg = "";
            cn.getCNX.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("delete tb_clientes where idCliente=@id", cn.getCNX);
                cmd.Parameters.AddWithValue("@id", id);
                

                int cantReg = cmd.ExecuteNonQuery();
                msg = cantReg.ToString() + " registro(s) eliminado(s)";

            }
            catch (SqlException ex)
            {
                msg = ex.Message.ToString();
            }
            finally { cn.getCNX.Close(); }

            return msg;
        }

        public Cliente obtenerRegistro(string id)
        {
            return listado().Where( c => c.Idcliente == id).FirstOrDefault();
        }

        public DataTable obtenerReporteCliente()
        {
            SqlDataAdapter da = new SqlDataAdapter("spGetAllClient", cn.getCNX);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}
