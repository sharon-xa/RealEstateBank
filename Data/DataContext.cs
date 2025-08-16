using Microsoft.EntityFrameworkCore;

using RealEstateBank.Data.Enums;
using RealEstateBank.Entities;

namespace RealEstateBank.Data;

public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Bank> Bank { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<AcceptedCitizen> AcceptedCitizens { get; set; }
    public DbSet<Complaint> Complaints { get; set; }
    public DbSet<Video> Videos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.HasPostgresEnum<Priority>();
        modelBuilder.HasPostgresEnum<UserRole>();
        modelBuilder.HasPostgresEnum<Gender>();

        modelBuilder.Entity<AppUser>(entity => {
            entity.HasIndex(u => u.Email).IsUnique(); // Unique Email
            entity.HasIndex(u => u.PhoneNumber).IsUnique(); // Unique Phone Number
            entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.PhoneNumber).IsRequired();
        });

        modelBuilder.Entity<Branch>(entity => {
            entity.Property(e => e.Lat).HasColumnType("numeric(9,6)");
            entity.Property(e => e.Long).HasColumnType("numeric(9,6)");

            entity
                .HasMany(b => b.AcceptedCitizens)               // Branch has many AcceptedCitizens
                .WithOne(ac => ac.Branch)                       // each AcceptedCitizen has one Branch
                .HasForeignKey(ac => ac.BranchId)               // foreign key in AcceptedCitizen
                .OnDelete(DeleteBehavior.NoAction);             // delete behavior
        });

        modelBuilder.Entity<Branch>();

        AppUser[] users = [
            new() {
                Id = Guid.Parse("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                FullName = "SuperAdmin",
                Role = UserRole.SuperAdmin,
                Email = "SuperAdmin@SuperAdmin.com",
                PasswordHash = "$2a$11$KsnJwz4qaejE6xIrGd.RLOfnA6TYRQrYKYACRb0/nQA8iBoi1PT7y",
                PhoneNumber = "07816562345",
                CreatedAt = new DateTime(2025, 8, 7, 0, 0, 0, DateTimeKind.Utc)
            }
        ];

        modelBuilder.Entity<AppUser>().HasData(users);

        Bank bank = new Bank { Id = 1 };
        modelBuilder.Entity<Bank>().HasData(bank);
    }
}
