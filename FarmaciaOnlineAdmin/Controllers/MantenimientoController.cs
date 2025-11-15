using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Configuration;
using System.IO;

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
        //CATEGORIAS
        #region Categorias


        [HttpGet]
        public JsonResult ListarCategoria()
        {
            List<Categoria> olista = new List<Categoria>();
            olista = new CN_Categoria().listar();

            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarCategoria(Categoria obj)
        {
            object resultado;
            string mensaje = string.Empty;

            //verificar si es nuevo o edicion
            if (obj.CategoriaID == 0)
            {
                resultado = new CN_Categoria().Registrar(obj, out mensaje);

            }
            //edicion
            else
            {

                resultado = new CN_Categoria().Editar(obj, out mensaje);

            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Categoria().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }



        #endregion


        //MARCAS 
        #region Marcas

        [HttpGet]
        public JsonResult ListarMarca()
        {
            List<Marca> olista = new List<Marca>();
            olista = new CN_Marcas().listar();

            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarMarca(Marca obj)
        {
            object resultado;
            string mensaje = string.Empty;

            //verificar si es nuevo o edicion
            if (obj.MarcaID == 0)
            {
                resultado = new CN_Marcas().Registrar(obj, out mensaje);

            }
            //edicion
            else
            {

                resultado = new CN_Marcas().Editar(obj, out mensaje);

            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Marcas().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        //MEDICAMENTOS
        #region Medicamentos

        [HttpGet]
        public JsonResult ListarMedicamentos()
        {
            List<Medicamentos> olista = new List<Medicamentos>();
            olista = new CN_Medicamentos().listar();
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarMedicamento(string obj, HttpPostedFileBase imagen)
        {
            
            string mensaje = string.Empty;
            bool operacionExitosa = true;
            bool guardar_imagen = true;
            Medicamentos oMedicamento = new Medicamentos();
            oMedicamento = Newtonsoft.Json.JsonConvert.DeserializeObject<Medicamentos>(obj);
            float precio;

            if (float.TryParse(oMedicamento.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-PE"), out precio))
            {
                oMedicamento.Precio = precio;
            }
            else
            {
                return Json(new { operacionExitosa = false, mensaje = "El formato del precio es incorrecto." }, JsonRequestBehavior.AllowGet);

            }


            //verificar si es nuevo o edicion
            if (oMedicamento.MedicamentoID == 0)
            {
              int idMedgenerado = new CN_Medicamentos().Registrar(oMedicamento, out mensaje);

                if(idMedgenerado != 0)
                {

                    oMedicamento.MedicamentoID = idMedgenerado;
                }
                else
                {
                    operacionExitosa = false;
                    
                }
            }
            //edicion
            else
            {
                operacionExitosa = new CN_Medicamentos().Editar(oMedicamento, out mensaje);
            }

            if (operacionExitosa) {
                //subir la imagen
                if (imagen != null)
                {
                    string ruta = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(imagen.FileName);
                    string nombre_imagen =string.Concat(oMedicamento.MedicamentoID.ToString(),extension);
                    try
                    {
                        imagen.SaveAs(Path.Combine(ruta,nombre_imagen));
                    }
                    catch (Exception ex)
                    {
                        guardar_imagen = false;
                       string msg= ex.Message;
                    }
                   
                    if (guardar_imagen)
                    {
                        oMedicamento.ImagenURL = ruta;
                        oMedicamento.NombreImagen = nombre_imagen;
                        bool respuesta = new CN_Medicamentos().GuardarImagen(oMedicamento, out mensaje);
                    }
                    else
                    {
                        mensaje = "El medicamento se guardó pero no se pudo guardar la imagen.";
                    }
                }

            }
            return Json(new { operacionExitosa = operacionExitosa, idMedgenerado=oMedicamento.MedicamentoID, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult imagenproducto(int id)
        {
            bool conversion;
            Medicamentos oMedicamento = new CN_Medicamentos().listar().Where(m => m.MedicamentoID== id).FirstOrDefault();

            string texto_base64 = CN_Recursos.ConvertirBase64(Path.Combine(oMedicamento.ImagenURL,oMedicamento.NombreImagen), out conversion);
            return Json(new { conversion = conversion, texto_base64 = texto_base64, extension= Path.GetExtension(oMedicamento.NombreImagen) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMedicamento(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Medicamentos().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
       

        #endregion

    }
}            
