using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using System.Web.Security;

namespace FarmaciaOnlineAdmin.Controllers
{
    public class AccesoController : Controller
    {
    
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CambiarClave()
        {
            return View();
        }
        public ActionResult Restablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {

            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuarios().listar().Where(u => u.Email == correo && u.Contrasena == CN_Recursos.convertirSha256(clave)).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrecta";
                return View();
            }

            else
            {
                if (oUsuario.Restablecer)
                {
                    TempData["UsuarioID"] = oUsuario.UsuarioID;
                    return RedirectToAction("CambiarClave");
                }

                FormsAuthentication.SetAuthCookie(oUsuario.Email, false);
            }
            ViewBag.Error = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarClave(string idusuario, string claveactual, string nuevaclave, string confirmarclave)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuarios().listar().Where(u => u.UsuarioID==int.Parse(idusuario)).FirstOrDefault();

            if (oUsuario.Contrasena != CN_Recursos.convertirSha256(claveactual))
            {
                TempData["UsuarioID"] = idusuario;
                ViewData["vContrasena"] = "";
                ViewBag.Error = "La contraseña actual es incorrecta";
                return View();
            }
            else if (nuevaclave != confirmarclave)
            {
                TempData["UsuarioID"] = idusuario;
                ViewData["vContrasena"] = claveactual;
                ViewBag.Error = "Las nuevas contraseñas no coinciden";
                return View();
            }
            ViewData["vContrasena"] = null;

            nuevaclave = CN_Recursos.convertirSha256(nuevaclave);
            string mensaje = string.Empty;
            bool respuesta = new CN_Usuarios().CambiarClave(int.Parse(idusuario), nuevaclave, out mensaje);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["UsuarioID"] = idusuario;
                ViewBag.Error = mensaje;
               return View();
            }         
                
        }

        [HttpPost]
        public ActionResult Restablecer(string correo)
        {
            Usuario ousuario = new Usuario();
            ousuario = new CN_Usuarios().listar().Where(item => item.Email == correo).FirstOrDefault();
            if (ousuario == null) 
            {
                ViewBag.Error = "No se encontro un usuario realcionado a ese correo";
                return View();
            }

            string mensaje = string.Empty;
            bool respuesta = new CN_Usuarios().RestablecerClave(ousuario.UsuarioID,correo, out mensaje);

            if (respuesta) 
            {
                ViewBag.Error = null;
                return RedirectToAction("Index"); 
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        public ActionResult cerrarsesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }
    }

}