using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objReporte = new CD_Reporte();

        public List<Reporte> Ventas(string fechaInicio, string fechaFin, string idtransaccion)
        {
            return objReporte.Ventas(fechaInicio, fechaFin, idtransaccion);
        }
        public Dashboard verDashboard()
        {
            return objReporte.verDashboard();
        }
    }
}
