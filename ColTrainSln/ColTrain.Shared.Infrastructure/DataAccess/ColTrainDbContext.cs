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
            modelBuilder.Entity<CityTable>()
                .HasOne(x => x.State)
                .WithMany(y => y.Cities)
                .HasForeignKey(x => x.StateId);


            base.OnModelCreating(modelBuilder);

        }

        public DbSet<CityTable> City { get; set; }
        public DbSet<StateTable> State { get; set; }

    }
}
