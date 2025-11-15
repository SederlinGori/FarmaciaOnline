using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Medicamentos
    {
        public List<Medicamentos> listar()
        {
            List<Medicamentos> lista = new List<Medicamentos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                   StringBuilder sb= new StringBuilder();
                     sb.AppendLine("select m.MedicamentoID, m.Nombre,m.Descripcion,");
                        sb.AppendLine("n.MarcaID,n.Descripcion[DesMarca], ");
                        sb.AppendLine("c.CategoriaID, c.Descripcion[DesCategoria],");
                        sb.AppendLine("m.Precio,m.Stock, m.ImagenURL, m.NombreImagen, m.activo");
                        sb.AppendLine("from Medicamentos m");
                        sb.AppendLine("inner join Marcas n on n.MarcaID = m.MarcaID");
                        sb.AppendLine("inner join Categorias c on c.CategoriaID = m.CategoriaID");
                       
                  
 
                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Medicamentos()
                            {
                                MedicamentoID = Convert.ToInt32(dr["MedicamentoID"]),
                                Nombre = dr["Nombre"].ToString(),                               
                                Descripcion = dr["Descripcion"].ToString(),
                                Activo = Convert.ToBoolean(dr["activo"]),
                                oMarca= new Marca()
                                {
                                    MarcaID = Convert.ToInt32(dr["MarcaID"]),
                                    Descripcion= dr["DesMarca"].ToString()
                                },
                                oCategoria= new Categoria()
                                {
                                    CategoriaID = Convert.ToInt32(dr["CategoriaID"]),
                                    Descripcion= dr["DesCategoria"].ToString()
                                },
                                Precio = Convert.ToSingle(dr["Precio"]),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                ImagenURL = dr["ImagenURL"].ToString(),
                                NombreImagen=dr["NombreImagen"].ToString()


                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Medicamentos>();
            }
            return lista;

        }

    

        public int Registrar(Medicamentos obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarMedicamento", oconexion);

                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("RequiereReceta", obj.RequiereReceta);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.AddWithValue("CategotiaID", obj.oCategoria.CategoriaID);
                    cmd.Parameters.AddWithValue("MarcaID", obj.oMarca.MarcaID);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }
   
        public bool Editar(Medicamentos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarMedicamento", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("RequiereReceta", obj.RequiereReceta);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.AddWithValue("CategotiaID", obj.oCategoria.CategoriaID);
                    cmd.Parameters.AddWithValue("MarcaID", obj.oMarca.MarcaID);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
        public bool GuardarImagen(Medicamentos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "update Medicamentos set ImagenURL = @ImagenURL, NombreImagen=@NombreImagen where MedicamentoID = @MedicamentoID";

                    SqlCommand cmd = new SqlCommand(query, oconexion);  
                    cmd.Parameters.AddWithValue("@ImagenURL", obj.ImagenURL);
                    cmd.Parameters.AddWithValue(" @MedicamentoID", obj.MedicamentoID); 
                    cmd.Parameters.AddWithValue(" @NombreImagen", obj.NombreImagen);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }

                    else
                    {
                        Mensaje = "No se pudo actualizar la imagen";
                    }

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarMedicamento", oconexion);
                    cmd.Parameters.AddWithValue("MedicamentoID ", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
