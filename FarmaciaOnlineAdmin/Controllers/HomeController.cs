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




    }
}