using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;

namespace FarmaciaOnlineAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult usuarios ()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerUsuarios()
        {
            List<Usuario> olista = new List<Usuario>();
            olista = new CN_Usuarios().listar();

            return Json( new { data = olista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuario obj)
        {
            object resultado;
            string mensaje = string.Empty;

            //verificar si es nuevo o edicion
            if (obj.UsuarioID == 0)
            {
               resultado= new CN_Usuarios().Registrar(obj, out mensaje);

            }
            //edicion
            else
            {
              
                resultado = new CN_Usuarios().Editar(obj, out mensaje);
               
            }
            return Json(new { resultado=resultado, mensaje= mensaje }, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Usuarios().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }



    }
}