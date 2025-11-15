using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Medicamentos
    {
       
        public int MedicamentoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }
        public int Stock { get; set; }
        public string ImagenURL { get; set; }
        public string NombreImagen { get; set; }
        public bool RequiereReceta { get; set; }
        public bool Activo { get; set; }
        public int CategoriaID { get; set; }
        public int marcaID { get; set; }
        public Marca oMarca { get; set; }
        public Categoria oCategoria { get; set; }
        public string PrecioTexto { get; set; }
        public string Base64Imagen { get; set; }
        public string ExtensionImagen { get; set; }



    }
}
