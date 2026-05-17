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
    }
}