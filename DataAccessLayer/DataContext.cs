using Entities.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataContext:DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-GV7I3N2\\SQLEXPRESS02;Database=FootballTeam; Trusted_Connection=True; MultipleActiveResultSets=true;");
        //}
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.UseSqlServer("Server=DESKTOP-GV7I3N2\\SQLEXPRESS02;Database=FootballTeam; Trusted_Connection=True; MultipleActiveResultSets=true;");
        //}

        public DataContext():base("Server=DESKTOP-GV7I3N2\\SQLEXPRESS02;Database=Football_Team; Trusted_Connection=True; MultipleActiveResultSets=true;") { }

        public DbSet<Player> Players { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public static DataContext Create()
        {
            return new DataContext();
        }
    }
}
