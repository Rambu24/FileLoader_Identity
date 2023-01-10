using FileLoader.Datos.Data.Repository.IRepository;
using FileLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader.Datos.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;

        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            DatosReporte = new DatosReporteRepository(_db);
            BalanceFocrede = new BalanceFocredeRepository(_db);
        }


        public IDatosReporteRepository DatosReporte { get; private set; }

        public IBalanceFocredeRepository BalanceFocrede { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
