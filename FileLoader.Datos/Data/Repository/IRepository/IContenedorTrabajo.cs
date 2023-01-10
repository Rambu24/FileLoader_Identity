using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader.Datos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        IDatosReporteRepository DatosReporte { get; }
        IBalanceFocredeRepository BalanceFocrede { get; }
        //Aquí se van agregando todos los repositorios

        void Save();
    }
}
