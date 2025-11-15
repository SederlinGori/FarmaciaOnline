using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Marca
    {
        public int MarcaID { get; set; }      
        public string Descripcion { get; set; }
        public bool activo { get; set; }
        public DateTime FechaRegisto { get; set; }
    }
}
