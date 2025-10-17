using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmaciaOnlineAdmin.Controllers
{
    public class MantenimientoController : Controller
    {
        // GET: Mantenimiento
        public ActionResult medicamentos()
        {
            return View();
        }
        public ActionResult pedidos()
        {
            return View();
        }
        public ActionResult categorias()
        {
            return View();
        }
        public ActionResult marcas()
        {
            return View();
        }
    }
}