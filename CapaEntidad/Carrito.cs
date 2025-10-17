using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Carrito
    {
        public int CarritoID { get; set; }
        public int UsuarioID { get; set; }      
        public DateTime FechaCreacion { get; set; }
    }
}
