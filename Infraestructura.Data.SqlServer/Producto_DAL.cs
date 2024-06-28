using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dominio.Entidades;
using System.Data.SqlClient;

namespace Infraestructura.Data.SqlServer
{
    public class Producto_DAL
    {
        Conexion_DAL cn = new Conexion_DAL();

        public List<Producto> listado()
        {
            // Creamos ListOf del tipo Producto
            List<Producto> lista = new List<Producto>();
            SqlCommand cmd = new SqlCommand("spGetAllProduct", cn.getCNX);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //Abrimos conexión
            cn.getCNX.Open();

            //Ejecutamos el SqlCommand
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Producto reg = new Producto();
                reg.IdProducto = dr.GetInt32(0);
                reg.NombreProducto = dr.GetString(1);
                reg.IdProveedor = dr.GetInt32(2);
                reg.IdCategoria = dr.GetInt32(3);
                reg.UMedida = dr.GetString(4);
                reg.PrecioUnidad = dr.GetDecimal(5);
                reg.UnidadesEnExistencia = dr.GetInt16(6);
                lista.Add(reg);
            }

            //Cerramos conexión
            dr.Close();
            cn.getCNX.Close();

            return lista;
        }

        public string Agregar(Producto reg)
        {
            string msg = "";
            cn.getCNX.Open();

            try 
            {
                SqlCommand cmd = new SqlCommand("spAddProduct", cn.getCNX);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idProducto", reg.IdProducto);
                cmd.Parameters.AddWithValue("@nombreProducto", reg.NombreProducto);
                cmd.Parameters.AddWithValue("@idProveedor", reg.IdProveedor);
                cmd.Parameters.AddWithValue("@idCategoria", reg.IdCategoria);
                cmd.Parameters.AddWithValue("@uMedida", reg.UMedida);
                cmd.Parameters.AddWithValue("@precioUnidad", reg.PrecioUnidad);
                cmd.Parameters.AddWithValue("@unidadesExistencia", reg.UnidadesEnExistencia);

                int cantReg = cmd.ExecuteNonQuery();
                msg = cantReg.ToString() + "registro(s) agregado(s)";

            } catch (SqlException ex) {
                msg = ex.Message.ToString();
            } finally { cn.getCNX.Close(); }

            return msg;
        }

        public string Actualizar(Producto reg)
        {
            string msg = "";
            cn.getCNX.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("spUpdateProduct", cn.getCNX);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idProducto", reg.IdProducto);
                cmd.Parameters.AddWithValue("@nombreProducto", reg.NombreProducto);
                cmd.Parameters.AddWithValue("@idProveedor", reg.IdProveedor);
                cmd.Parameters.AddWithValue("@idCategoria", reg.IdCategoria);
                cmd.Parameters.AddWithValue("@uMedida", reg.UMedida);
                cmd.Parameters.AddWithValue("@precioUnidad", reg.PrecioUnidad);
                cmd.Parameters.AddWithValue("@unidadesExistencia", reg.UnidadesEnExistencia);

                int cantReg = cmd.ExecuteNonQuery();
                msg = cantReg.ToString() + "registro(s) actualizado(s)";

            }
            catch (SqlException ex)
            {
                msg = ex.Message.ToString();
            }
            finally { cn.getCNX.Close(); }

            return msg;
        }

        public int Eliminar(int id)
        {
            int result = 0;
            cn.getCNX.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("spDeleteProduct", cn.getCNX);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idProducto", id);

                int cantReg = cmd.ExecuteNonQuery();
                result = cantReg;

            } catch (SqlException ex)
            {
                result = -1;
            }
            finally
            {
                cn.getCNX.Close();
            }
            return result;

        }

        public int obtenerSecuenciaIdProducto()
        {
            int secuencia = 0;
            cn.getCNX.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("spGetProductSequence", cn.getCNX);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                secuencia = (Int32)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                cn.getCNX.Close();
            }

            return secuencia;
        }

        

        public Producto obtenerRegistro(int id)
        {
            return listado().Where(c => c.IdProducto == id).FirstOrDefault();
        }


    }
}
