using FileLoader.Datos.Data.Repository.IRepository;
using FileLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader.Datos.Data.Repository
{
   

    public class BalanceFocredeRepository : Repository<BalanceFocredeCLS>, IBalanceFocredeRepository
    {
        private readonly ApplicationDbContext _db;

        public BalanceFocredeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(BalanceFocredeCLS balanceFocrede)
        {
            var objDesdeDb = _db.BalanceFocrede.FirstOrDefault(s => s.Indice == balanceFocrede.Indice);


            _db.SaveChanges();
        }
    }
}
