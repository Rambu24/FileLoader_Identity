using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader.Models
{
    public class Catalogo_Cuentas_FocredeCLS
    {
        [Key]
        public int CodigoCuenta { get;  set; }
        public string NombreCuenta { get; set; }

    }
}
