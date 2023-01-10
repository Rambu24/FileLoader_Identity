using FileLoader.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileLoader.Datos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options)
            : base(options)
        {
        }

        public DbSet<BalanceFocredeCLS> BalanceFocrede { get; set; }        
        public DbSet<DatosReporteCLS> DatosReporte { get; set; }
        public DbSet<Catalogo_Cuentas_FocredeCLS> Catalogo_CuentasFocrede { get; set; }
        public DbSet<Catalogo_ReportesCLS> Catalogo_Reportes { get; set; }
        //public DbSet<Scalar_Values> ScalarValue { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Scalar_Values>().HasNoKey();
        //}

    }
}