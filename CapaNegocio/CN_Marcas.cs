using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;



namespace CapaNegocio
{
    public class CN_Marcas
    {
        private CD_Marca objCapaDatos = new CD_Marca();
        public List<Marca> listar()
        {
            return objCapaDatos.listar();
        }

        public int Registrar(Marca obj, out string Mensaje)
        {
            //Validar que los campos no esten vacios
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la marca no puede estar vacío.";
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

        public bool Editar(Marca obj, out string Mensaje)
        {
            //validar que los campos no esten vacios
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la marca  no puede estar vacío.";
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
