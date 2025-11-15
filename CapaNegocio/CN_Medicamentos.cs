using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Medicamentos
    {
        
            private CD_Medicamentos objCapaDatos = new CD_Medicamentos();
            public List<Medicamentos> listar()
            {
                return objCapaDatos.listar();
            }

            public int Registrar(Medicamentos obj, out string Mensaje)
            {
                //Validar que los campos no esten vacios
                Mensaje = string.Empty;

                if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Descripcion))
                {
                    Mensaje = "El nombre del medicamento no puede estar vacío.";
                }

                else  if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
                {
                Mensaje = "La descripcion del medicamento no puede estar vacío.";
                }

                else if (obj.oMarca.MarcaID ==0)
                {
                    Mensaje = "Debe seleccionar una marca";
                }
                else if (obj.oCategoria.CategoriaID ==0)
                {
                    Mensaje = "Debe seleccionar una categoria";
                }
                else if (obj.Precio ==0)
                {
                    Mensaje = "Debe ingresar el precio";
                }
                else if (obj.Stock ==0)
                {
                    Mensaje = "Debe ingresar el stock";
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

            public bool Editar(Medicamentos obj, out string Mensaje)
            {
                //validar que los campos no esten vacios
                Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "El nombre del medicamento no puede estar vacío.";
            }

            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion del medicamento no puede estar vacío.";
            }

            else if (obj.oMarca.MarcaID == 0)
            {
                Mensaje = "Debe seleccionar una marca";
            }
            else if (obj.oCategoria.CategoriaID == 0)
            {
                Mensaje = "Debe seleccionar una categoria";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el precio";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "Debe ingresar el stock";
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

            public bool GuardarImagen(Medicamentos obj, out string Mensaje)
            {
                return objCapaDatos.GuardarImagen(obj, out Mensaje);

            }
            public bool Eliminar(int id, out string Mensaje)
            {
                return objCapaDatos.Eliminar(id, out Mensaje);
            }


        }
    }
