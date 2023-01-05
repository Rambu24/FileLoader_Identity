using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DatosReporteCLS
    {
        public int Indice { get; set; }
        public int Moneda { get; set; }
        public DateTime Fecha_Reporte { get; set; }
        public string? Usuario_Generador { get; set; }

        public List<BalanceFocredeCLS> listadoFocrede { get; set; }

    }
}
