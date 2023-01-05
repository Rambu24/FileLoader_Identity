using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class BalanceFocredeBL
    {

        public async Task<string> InsertarFocrede(DatosReporteCLS datosReporteCLS)
        {
            BalanceFocredeDAL balanceFocredeDAL = new BalanceFocredeDAL();

            return balanceFocredeDAL.InsertarFocrede(datosReporteCLS);
        }


    }
}