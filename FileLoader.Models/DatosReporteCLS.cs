using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader.Models
{
    public class DatosReporteCLS
    {

        [Key]
        public int Indice { get; set; }       
        public int CodigoReporte { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public DateTime Fecha_Reporte { get; set; }
        public string? Usuario_Generador { get; set; }

        public List<BalanceFocredeCLS> listadoFocrede { get; set; }

        public DatosReporteCLS()
        {
            this.Fecha_Registro = DateTime.Now;            
        }

        [ForeignKey("CodigoReporte")]
        public Catalogo_ReportesCLS Catalogo_ReportesCLS { get; set; }
        public bool Active { get; set; }

    }
}
