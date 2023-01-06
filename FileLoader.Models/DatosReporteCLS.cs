using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader.Models
{
    public class DatosReporteCLS
    {

        [Key]
        public int Indice { get; set; }
        [Required]
        public int Moneda { get; set; }
        public DateTime Fecha_Reporte { get; set; }
        public string? Usuario_Generador { get; set; }

        //public List<BalanceFocredeCLS> listadoFocrede { get; set; }

    }
}
