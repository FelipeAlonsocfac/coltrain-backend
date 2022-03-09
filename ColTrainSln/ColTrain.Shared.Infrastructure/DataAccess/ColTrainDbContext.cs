using ColTrain.Shared.DTO.Models;
using Microsoft.EntityFrameworkCore;

namespace ColTrain.Shared.Infrastructure.DataAccess
{
    public class ColTrainDbContext : DbContext
    {
        public ColTrainDbContext(DbContextOptions<ColTrainDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<MunicipioTable>()
            //    .HasOne(x => x.Departamento)
            //    .WithMany(y => y.Municipios)
            //    .HasForeignKey(x => x.IdDepartamento);


            base.OnModelCreating(modelBuilder);

        }

        public DbSet<CityTable> City { get; set; }

    }
}
