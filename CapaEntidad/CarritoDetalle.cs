using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CarritoDetalle
    {    
		public int CarritoDetalleID { get; set; }
		public int CarritoID { get; set; }
		public int MedicamentoID { get; set; }
		public int Cantidad { get; set; }
		public float PrecioUnitario { get; set; }
		public float ITBIS { get; set; }

    }
}
