using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileLoader.Models
{
    public class BalanceFocredeCLS
    {
        [Key]
        public int Indice { get; set; }        
        public int? Id_Reporte { get; set; } //Llave foránea                
        public string CodigoCuenta { get; set; } //Llave foránea
        public string NombreCuenta { get; set; }
        public decimal SaldoAlDia { get; set; }
        public decimal SaldoDiaAnterior { get; set; }
        public decimal Variacion { get; set; }
        

    }
}