using gosarajevovol3.Models;
using Microsoft.AspNetCore.Identity;

namespace gosarajevovol3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<RegisteredUser> RegisteredUsers { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Operator> Operators { get; set; }
    public DbSet<Attraction> Attractions { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Hospitality> Hospitality { get; set; }
    public DbSet<SmartPlanner> SmartPlanners { get; set; }
    public DbSet<Preference> Preferences { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Newsletter> Newsletters { get; set; }
    public DbSet<Coordinates> Coordinates { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().ToTable("Users");

        modelBuilder.Entity<SmartPlanner>()
            .HasOne(s => s.Preference)
            .WithMany()
            .HasForeignKey(s => s.PreferenceId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<SmartPlanner>()
            .HasOne(s => s.RegisteredUser)
            .WithMany(u => u.PlannerList)
            .HasForeignKey(s => s.RegisteredUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<RegisteredUser>()
            .HasOne(u => u.Preference)
            .WithOne(p => p.RegisteredUser)
            .HasForeignKey<Preference>(p => p.RegisteredUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<RegisteredUser>("RegisteredUser")
            .HasValue<Admin>("Admin")
            .HasValue<Operator>("Operator");
        var hasher = new PasswordHasher<User>();
        var admin = new Admin
        {
            Id = 1,
            UserName = "admin@gmail.com",
            NormalizedUserName = "ADMIN@GMAIL.COM",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };
        admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");
        var operater = new Operator
        {
            Id = 2,
            UserName = "operator@gmail.com",
            NormalizedUserName = "OPERATOR@GMAIL.COM",
            Email = "operator@gmail.com",
            NormalizedEmail = "OPERATOR@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        operater.PasswordHash = hasher.HashPassword(operater, "Operator123!");

        var testUser = new RegisteredUser
        {
            Id = 3,
            UserName = "korisnik@gmail.com",
            NormalizedUserName = "KORISNIK@GMAIL.COM",
            Email = "korisnik@gmail.com",
            NormalizedEmail = "KORISNIK@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        testUser.PasswordHash = hasher.HashPassword(testUser, "Korisnik123!");
        var testUser1 = new RegisteredUser
        {
            Id = 4,
            UserName = "duderija.amina2@gmail.com",
            NormalizedUserName = "DUDERIJA.AMINA2@GMAIL.COM",
            Email = "duderija.amina2@gmail.com",
            NormalizedEmail = "DUDERIJA.AMINA2@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        testUser1.PasswordHash = hasher.HashPassword(testUser1, "Amina123!");
        modelBuilder.Entity<Admin>().HasData(admin);
        modelBuilder.Entity<Operator>().HasData(operater);
        modelBuilder.Entity<RegisteredUser>().HasData(testUser);
        modelBuilder.Entity<RegisteredUser>().HasData(testUser1);
    }
}