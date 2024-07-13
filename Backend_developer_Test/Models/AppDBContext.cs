using Backend_developer_Test.Models.Database;
using Microsoft.EntityFrameworkCore;


namespace Backend_developer_Test.Models
{
    public partial class AppDBContext : DbContext 
    {
        public AppDBContext()
        {
            
        }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Department> Departments  { get; set; }
        public virtual DbSet<SubDepartments> SubDeprtments  { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=41.60.245.81;Database=demo;User Id=sa;Password=AKDNeHRC.123;TrustServerCertificate=true;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            OnModelCreatingPartial(modelBuilder);
            //Seed.Seed.Run(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
