using DataBaseContext.Models;
using DataBaseContext.Models.Client;
using DataBaseContext.Models.Contact;
using DataBaseContext.Models.Good;
using Microsoft.EntityFrameworkCore;

namespace DataBaseContext.EF
{
    public class Context : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB; Database=Renta; Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(new Client()
            {
                Id = 1,
                Firstname = "admin",
                Lastname = "admin",
                Password = "admin",
            });
        }
    }
}
