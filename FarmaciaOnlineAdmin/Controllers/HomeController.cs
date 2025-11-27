using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace FarmaciaOnlineAdmin.Controllers
{
    [Authorize]
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


      

        [HttpGet]
        public JsonResult ListaReporte(string fechainico,string fechafin,string idtransaccion)
        {
            List<Reporte> olista = new List<Reporte>();
            olista = new CN_Reporte().Ventas(fechainico,fechafin,idtransaccion);
            return Json(new{ data=olista}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult VerDashboard()
        {
            Dashboard obj = new CN_Reporte().verDashboard();
            return Json(new{ resultado=obj}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportarReporte(string fechainico, string fechafin, string idtransaccion)
        {
           List<Reporte> olista = new List<Reporte>();
            olista = new CN_Reporte().Ventas(fechainico, fechafin, idtransaccion);
            
            DataTable dt = new DataTable();           
            dt.Columns.Add("Fecha Venta", typeof(string));
            dt.Columns.Add("Usuario", typeof(string));
            dt.Columns.Add("Medicamento", typeof(string));
            dt.Columns.Add("Precio", typeof(float));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Total", typeof(float));
            dt.Columns.Add("Id Transaccion", typeof(string));
            foreach (Reporte r in olista)
            {
                dt.Rows.Add(new object[]{
                r.FechaVenta,
                r.Usuario,
                r.Medicamento,
                r.precio,
                r.Cantidad,
                r.Total,
                r.Idtransaccion
                });
            }

            dt.TableName = "Datos";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVentas" + DateTime.Now.ToString() + ".xlsx");
                }
            }


        }


    }
}