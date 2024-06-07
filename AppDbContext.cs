using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Yampol
{
  using Lab2_Yampol.Models;
  using System.Data.Entity;

  public class AppDbContext : DbContext
  {
    public AppDbContext() : base("name=AppDbContext")
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<DeclineReason> DeclineReasons { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      //Конфігурація ключів
      modelBuilder.Entity<Order>()
          .HasKey(o => o.Id);
      modelBuilder.Entity<DeclineReason>()
          .HasKey(dr => dr.Id);
      modelBuilder.Entity<Car>()
          .HasKey(c => c.Id);

      //Зв'язки
      modelBuilder.Entity<Order>()
          .HasRequired(o => o.Car)
          .WithMany(c => c.Orders)
          .HasForeignKey(o => o.CarId);

      modelBuilder.Entity<DeclineReason>()
          .HasRequired(dr => dr.Car)
          .WithMany(c => c.DeclineReasons)
          .HasForeignKey(dr => dr.CarId);
    }
  }
}
