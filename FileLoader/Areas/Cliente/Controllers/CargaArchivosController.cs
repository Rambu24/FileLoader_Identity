using FileLoader.Datos.Data.Repository.IRepository;
using FileLoader.Datos;
using FileLoader.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileLoader.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class CargaArchivosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CargaArchivosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

               
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DatosReporteCLS datosReporteCLS)
        {
            BalanceFocredeDAL oDatosDAL = new BalanceFocredeDAL();

            string resultado = oDatosDAL.InsertarFocrede(datosReporteCLS);

            //Write your Insert code here;
            return Ok(resultado);
        }
    }
}
