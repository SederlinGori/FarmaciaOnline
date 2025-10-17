using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Pagos
    {
        
		public int PagoID { get; set; }
		public int PedidoID { get; set; }
		public float Monto { get; set; }
		public string MetodoPago { get; set; }
		public string EstadoPago { get; set; }
		public DateTime FechaPago { get; set; }
		public float ITBIS { get; set; }
    }
}
