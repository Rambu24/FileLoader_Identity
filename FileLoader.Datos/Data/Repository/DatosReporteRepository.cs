using FileLoader.Datos.Data.Repository.IRepository;
using FileLoader.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader.Datos.Data.Repository
{
    public class DatosReporteRepository : Repository<DatosReporteCLS>, IDatosReporteRepository
    {
        private readonly ApplicationDbContext _db;

        public DatosReporteRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;            
        }

        public void Update(DatosReporteCLS datosreporte)
        {
            var objDesdeDb = _db.DatosReporte.FirstOrDefault(s => s.Indice == datosreporte.Indice);
            objDesdeDb.Fecha_Reporte = datosreporte.Fecha_Reporte;   

            _db.SaveChanges();
        }

    }
}
