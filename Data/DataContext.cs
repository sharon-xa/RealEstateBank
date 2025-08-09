using Microsoft.EntityFrameworkCore;

using RealEstateBank.Data.Enums;
using RealEstateBank.Entities;

namespace RealEstateBank.Data;

public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {
    }

    public DbSet<AppUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        AppUser[] users = [
            new() {
                Id = Guid.Parse("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                FullName = "SuperAdmin",
                Role = UserRole.SuperAdmin,
                Email = "SuperAdmin@SuperAdmin.com",
                PasswordHash = "$2a$11$or2Rg8NDeqq10APfQng1HO9zmM.LpqLc92QAR79ssJcgulD.HJgsu",
                PhoneNumber = "07816562345",
                CreationDate = new DateTime(2025, 8, 7, 0, 0, 0, DateTimeKind.Utc)
            }
        ];

        modelBuilder.Entity<AppUser>().HasData(users);
    }
}
