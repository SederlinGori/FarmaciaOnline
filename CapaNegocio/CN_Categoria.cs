using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Categoria
    {

        private CD_Categoria objCapaDatos = new CD_Categoria();

        public List<Categoria> listar()
        {
            return objCapaDatos.listar();
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            //Validar que los campos no esten vacios
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la categoria  no puede estar vacío.";
            }
       

            if (string.IsNullOrEmpty(Mensaje))
            {
          

              
                    return objCapaDatos.Registrar(obj, out Mensaje);
            }
            else
            {
                
                    return 0;
            }






            

        }

        public bool Editar(Categoria obj, out string Mensaje)
        {
            //validar que los campos no esten vacios
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la categoria  no puede estar vacío.";
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
