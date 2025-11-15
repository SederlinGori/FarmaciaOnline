using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;
using System.Web;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_usuarios objCapaDatos = new CD_usuarios();
       
        public List<Usuario> listar()
        {
            return objCapaDatos.listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            //Validar que los campos no esten vacios
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del usuario no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Mensaje = "El email del usuario no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(Mensaje)) {





                string clave = CN_Recursos.Generarclave();
                string asunto = "Creación de cuenta exitosa";
                string mensaje_correo = "<h3>Su cuenta ha sido creada exitosamente</h3><br></br><p>Su contraseña es: <strong>" + clave + "</strong></p>";
                mensaje_correo=mensaje_correo.Replace("!clave!",clave);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.Email, asunto, mensaje_correo);

                if (respuesta)
                {
                    obj.Contrasena = CN_Recursos.convertirSha256(clave);
                    return objCapaDatos.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo electrónico.";
                    return 0;
                }






            }
            else
            {
                return 0;
            }

        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            //validar que los campos no esten vacios
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del usuario no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Mensaje = "El email del usuario no puede estar vacío.";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDatos.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDatos.Eliminar(id, out Mensaje);
        }
    }
}
