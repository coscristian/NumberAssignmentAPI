using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DB
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientRuffleAsignation> ClientRuffleAsignations { get; set; }
        public DbSet<Raffle> Raffles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
