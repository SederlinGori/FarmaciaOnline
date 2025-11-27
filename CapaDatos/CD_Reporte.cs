using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Reporte
    {

        public List<Reporte> Ventas(string fechaInicio, string fechaFin, string idtransaccion)
        {
            List<Reporte> lista = new List<Reporte>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    
                    SqlCommand cmd = new SqlCommand("sp_reporteVentas", oconexion);
                    
                    cmd.Parameters.AddWithValue("fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("fechaFin",fechaFin);
                    cmd.Parameters.AddWithValue("IdTransaccion", idtransaccion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Reporte()
                            {
                                FechaVenta = dr["FechaVenta"].ToString(),
                                Usuario = dr["Usuario"].ToString(),
                                Medicamento = dr["Medicamento"].ToString(),
                                precio = float.Parse("Precio"),
                                Cantidad = Convert.ToInt32( dr["Cantidad"].ToString()),
                                Total = float.Parse("Total"),                                
                                Idtransaccion = dr["Idtransaccion"].ToString(),
                               

                            });
                        }
                    }
                }


            }
            catch
            {
                lista = new List<Reporte>();
            }
            return lista;

        }


        public Dashboard verDashboard()
        {
           Dashboard obj = new Dashboard();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    
                    SqlCommand cmd = new SqlCommand("sp_reporteDashboard", oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            obj = new Dashboard()
                            {
                                TotalUsuarios = Convert.ToInt32(dr["TotalUsuarios"]),
                                TotalVentas = Convert.ToInt32(dr["TotalVenta"]),
                                TotalMedicamentos = Convert.ToInt32(dr["TotalMedicamentos"])
                            };

                           
                        }
                    }
                }


            }
            catch
            {
                obj = new Dashboard();
            }
            return obj;

        }


    }
}
