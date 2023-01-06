using FileLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader.Datos.Data.Repository.IRepository
{
    public interface IDatosReporteRepository : IRepository<DatosReporteCLS>
    {
        void Update(DatosReporteCLS datosreporte);

    }
}
