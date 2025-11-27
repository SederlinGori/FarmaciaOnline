using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Cliente
    {

        public int Registrar(Cliente obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("genero", obj.genero);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Contrasena", obj.Contrasena);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Cedula", obj.Cedula);                  
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


        public List<Cliente> listar()
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT ClienteID, Nombre,genero, Email, Contrasena, Direccion, Telefono, FechaRegistro, Cedula, Restablecer  FROM clientes";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                ClienteID = Convert.ToInt32(dr["ClienteID"]),
                                Nombre = dr["Nombre"].ToString(),
                                Email = dr["Email"].ToString(),
                                Contrasena = dr["Contrasena"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                                Cedula = dr["Cedula"].ToString(),                                
                                genero = Convert.ToInt32(dr["genero"]),
                                Restablecer = dr["Restablecer"] == DBNull.Value ? false : Convert.ToBoolean(dr["Restablecer"])


                            });
                        }
                    }
                }


            }
            catch
            {
                lista = new List<Cliente>();
            }
            return lista;

        }

        public bool CambiarClave(int idcliente, string nuevaclave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update clientes set contrasena= @nuevaclave, Restablecer = 0 where Usuarioid= @idcliente ", oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.Parameters.AddWithValue("@nuevaclave", nuevaclave);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool RestablecerClave(int idcliente, string clave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update clientes set contrasena= @clave, Restablecer = 1 where clienteid= @idcliente ", oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);

                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

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
