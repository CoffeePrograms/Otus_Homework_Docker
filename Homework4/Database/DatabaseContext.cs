using Homework4.Data;
using Homework4.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Homework4.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AddEntities(modelBuilder);

            Seed(modelBuilder);
        }

        private void AddEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>().HasKey(nameof(BaseEntity.Id));
            modelBuilder.Entity<WeatherForecast>().Property(r => r.Date).IsRequired();
            modelBuilder.Entity<WeatherForecast>().Property(r => r.TemperatureC).IsRequired();
            modelBuilder.Entity<WeatherForecast>().Property(r => r.Summary).HasMaxLength(200);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>().HasData(FakeDataFactory.WeatherForecasts());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
