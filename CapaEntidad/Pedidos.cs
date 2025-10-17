using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Pedidos
    {
      
        public int PedidoID { get; set; }
        public int UsuarioID { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }
        public string DireccionEntrega { get; set; }
        public float Total { get; set; }
    }
}
