using FileLoader.Datos.Data.Repository.IRepository;
using FileLoader.Datos;
using FileLoader.Models;
using Microsoft.AspNetCore.Mvc;
using FileLoader.Datos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using ChartJSCore.Helpers;
using ChartJSCore.Models;

namespace FileLoader.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class CargaArchivosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;

        public CargaArchivosController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _context = context;
        }

        public IActionResult Index()
        {
            Chart chart = GenerateLineChart();
            Chart verticalBarChart = GenerateVerticalBarChart();

            ViewData["VerticalBarChart"] = verticalBarChart;
            ViewData["chart"] = chart;

            IEnumerable<DatosReporteCLS> oListadoReportes;
            int cantidadReportes = 0;
            try
            {
                oListadoReportes = _contenedorTrabajo.DatosReporte.GetAll(x => x.Active == true, orderBy: null, "Catalogo_ReportesCLS");
                cantidadReportes = oListadoReportes.Count();
            }
            catch (Exception)
            {

                oListadoReportes = null;
            }

            ViewBag.ConteoReportesTotal = cantidadReportes;
          


            return View(oListadoReportes);
        }

        private static Chart GenerateLineChart()
        {
            Chart chart = new Chart();

            chart.Type = Enums.ChartType.Line;

            Data data = new Data();
            data.Labels = new List<string>() { "January", "February", "March", "April", "May", "June", "July" };

            LineDataset dataset = new LineDataset()
            {
                Label = "My First dataset",
                Data = new List<double?> { 65, 59, 80, 81, 56, 55, 40 },
                Fill = "false",
                Tension = 0.1,
                BackgroundColor = new List<ChartColor> { ChartColor.FromRgba(75, 192, 192, 0.4) },
                BorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                BorderCapStyle = "butt",
                BorderDash = new List<int> { },
                BorderDashOffset = 0.0,
                BorderJoinStyle = "miter",
                PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
                PointBorderWidth = new List<int> { 1 },
                PointHoverRadius = new List<int> { 5 },
                PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) },
                PointHoverBorderWidth = new List<int> { 2 },
                PointRadius = new List<int> { 1 },
                PointHitRadius = new List<int> { 10 },
                SpanGaps = false
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;
            return chart;
        }

        private static Chart GenerateVerticalBarChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Bar;

            Data data = new Data();
            data.Labels = new List<string>() { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" };

            BarDataset dataset = new BarDataset()
            {
                Label = "# of Votes",
                Data = new List<double?>() { 12, 19, 3, 5, 2, 3 },
                BackgroundColor = new List<ChartColor>
                {
                    ChartColor.FromRgba(255, 99, 132, 0.2),
                    ChartColor.FromRgba(54, 162, 235, 0.2),
                    ChartColor.FromRgba(255, 206, 86, 0.2),
                    ChartColor.FromRgba(75, 192, 192, 0.2),
                    ChartColor.FromRgba(153, 102, 255, 0.2),
                    ChartColor.FromRgba(255, 159, 64, 0.2)
                },
                BorderColor = new List<ChartColor>
                {
                    ChartColor.FromRgb(255, 99, 132),
                    ChartColor.FromRgb(54, 162, 235),
                    ChartColor.FromRgb(255, 206, 86),
                    ChartColor.FromRgb(75, 192, 192),
                    ChartColor.FromRgb(153, 102, 255),
                    ChartColor.FromRgb(255, 159, 64)
                },
                BorderWidth = new List<int>() { 1 },
                BarPercentage = 0.5,
                BarThickness = 6,
                MaxBarThickness = 8,
                MinBarLength = 2
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            var options = new Options
            {
                Scales = new Dictionary<string, Scale>()
                {
                    { "y", new CartesianLinearScale()
                        {
                            BeginAtZero = true
                        }
                    },
                    { "x", new Scale()
                        {
                            Grid = new Grid()
                            {
                                Offset = true
                            }
                        }
                    },
                }
            };

            chart.Options = options;

            chart.Options.Layout = new Layout
            {
                Padding = new Padding
                {
                    PaddingObject = new PaddingObject
                    {
                        Left = 10,
                        Right = 12
                    }
                }
            };

            return chart;
        }

        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]        
        public async Task<IActionResult> Create(DatosReporteCLS datosReporteCLS)
        {
            BalanceFocredeDAL oDatosDAL = new BalanceFocredeDAL();
            string resultado;
            
            try
            {
                datosReporteCLS.Usuario_Generador = "LCHINCHILLAB";
                if (datosReporteCLS.Indice == 0)
                {                    

                    _contenedorTrabajo.DatosReporte.Add(datosReporteCLS);
                    _contenedorTrabajo.Save();
                    string id = datosReporteCLS.Indice.ToString();
                    
                    //string resultado = oDatosDAL.InsertarFocrede(datosReporteCLS);
                    resultado = id;

                }
                else
                {
                    List<BalanceFocredeCLS> reporteAdicional = new List<BalanceFocredeCLS>();
                    foreach (var item in datosReporteCLS.listadoFocrede)
                    {
                        item.Id_Reporte = datosReporteCLS.Indice;
                        _contenedorTrabajo.BalanceFocrede.Add(item);
                    };
                    _contenedorTrabajo.Save();
                    resultado = datosReporteCLS.Indice.ToString();
                }


                return Ok(resultado);
            }
            catch (Exception e)
            {
                resultado = "Sucedió un error al realizar la inserción";
                return BadRequest(resultado);
            }
           
        }
    }
}
