using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Reporte
    {
        public string FechaVenta { get; set; }
        public string Usuario { get; set; }
        public string Medicamento { get; set; }
        public float Total { get; set; }
        public float precio { get; set; }
        public string Idtransaccion { get; set; }   
        public int Cantidad { get; set; }
    }
}
