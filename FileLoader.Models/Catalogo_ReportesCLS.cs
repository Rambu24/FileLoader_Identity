using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader.Models
{
    public class Catalogo_ReportesCLS
    {
        [Key]
        public int Codigo_Reporte { get; set; }
        public string Nombre_Reporte { get; set; }
        public bool Active { get; set; }

        public IEnumerable<DatosReporteCLS> DatosReporteCLS { get; set; }

    }
}
