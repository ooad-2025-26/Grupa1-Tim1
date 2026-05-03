using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MigracijaNaBazu.Models;

namespace MigracijaNaBazu.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options) {
        public DbSet<RegistrovaniKorisnik> RegistrovaniKorisnici { get; set; }
        public DbSet<Admin> Admini { get; set; }
        public DbSet<Operater> Operateri { get; set; }

        public DbSet<Event> Eventi { get; set; }
        public DbSet<Atrakcija> Atrakcije { get; set; }
        public DbSet<UgostiteljskiObjekat> UgostiteljskiObjekti { get; set; }
        public DbSet<Recenzija> Recenzije { get; set; }
        public DbSet<Koordinate> Koordinate { get; set; }

        public DbSet<Newsletter> Newsletteri { get; set; }
        public DbSet<SmartPlanner> SmartPlanneri { get; set; }
        public DbSet<Preferencija> Preferencije { get; set; }
    }
}
