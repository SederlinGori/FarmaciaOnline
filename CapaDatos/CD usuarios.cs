using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_usuarios
    {
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT UsuarioID, Nombre, Email, Contrasena, Direccion, Telefono, FechaRegistro, Cedula, Activo FROM Usuarios";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                                Nombre = dr["Nombre"].ToString(),
                                Email = dr["Email"].ToString(),
                                Contrasena = dr["Contrasena"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                                Cedula = dr["Cedula"].ToString(),
                               Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                    }
                }


            }
            catch 
            {
                lista = new List<Usuario>();
            }
            return lista;

        }
    }
}
