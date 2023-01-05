using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace FileLoader.Controllers
{
    public class CargaArchivosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> InsertarFocrede(DatosReporteCLS datosReporteCLS)
        {            
            BalanceFocredeBL oDatosBL = new BalanceFocredeBL();

            string resultado = await oDatosBL.InsertarFocrede(datosReporteCLS);

            //Write your Insert code here;
            return Ok(resultado);
        }



    }

    
}
