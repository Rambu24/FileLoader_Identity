namespace CapaEntidad
{
    public class BalanceFocredeCLS
    {
        public int Id_Reporte { get; set; } //Llave foránea        
        public int CodigoCuenta { get; set; }
        public decimal SaldoAlDia { get; set; }
        public decimal SaldoDiaAnterior { get; set; }
        public decimal Variacion { get; set; }

    }
}