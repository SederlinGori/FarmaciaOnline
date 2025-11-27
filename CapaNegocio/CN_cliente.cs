using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_cliente
    {
        private CD_Cliente objCapaDatos = new CD_Cliente();

        public List<Cliente> listar()
        {
            return objCapaDatos.listar();
        }

        public int Registrar(Cliente obj, out string Mensaje)
        {
            //Validar que los campos no esten vacios
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Mensaje = "El email no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                obj.Contrasena = CN_Recursos.convertirSha256(obj.Contrasena);
                return objCapaDatos.Registrar(obj, out Mensaje);

                
            }
            else
            {
                return 0;
            }

        }      

       

        public bool CambiarClave(int idcliente, string nuevaclave, out string Mensaje)
        {
            return objCapaDatos.CambiarClave(idcliente, nuevaclave, out Mensaje);
        }



        public bool RestablecerClave(int idcliente, string correo, out string Mensaje)
        {

            Mensaje = string.Empty;
            string nuevaClave = CN_Recursos.Generarclave();
            bool resultado = objCapaDatos.RestablecerClave(idcliente, CN_Recursos.convertirSha256(nuevaClave), out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña restablecida";
                string mensaje_correo = "<h3>Su cuenta ha sido restablecida exitosamente</h3><br></br><p>Su contraseña es !clave!  </p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaClave);

                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo electrónico.";
                    return false;
                }
            }

            else
            {
                Mensaje = "No se pudo restabelcer la contraseña.";
                return false;
            }


        }
    }
}
