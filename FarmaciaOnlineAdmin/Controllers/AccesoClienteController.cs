using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FarmaciaOnlineAdmin.Controllers
{
    public class AccesoClienteController : Controller    {
        // GET: AccesoCliente
       
        public ActionResult Registrar()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult Registrar(Cliente obj)
        {
            int resultado;
            string mensaje = string.Empty;

            ViewData["Nombre"] = string.IsNullOrEmpty( obj.Nombre);
            ViewData["Email"] = string.IsNullOrEmpty( obj.Email);
           
            

            if (obj.Contrasena != obj.ConfirmarContrasena)
            {
               ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            resultado = new CN_cliente().Registrar(obj, out mensaje);

            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
            
        }         
        
    }
}