namespace CapaEntidad
{
    public class BalanceFocredeCLS
    {
        public int? Id_Reporte { get; set; } //Llave foránea                
        public string CodigoCuenta { get; set; } //Llave foránea
        public string NombreCuenta { get; set; }
        public decimal SaldoAlDia { get; set; }
        public decimal SaldoDiaAnterior { get; set; }
        public decimal Variacion { get; set; }
        

    }
}