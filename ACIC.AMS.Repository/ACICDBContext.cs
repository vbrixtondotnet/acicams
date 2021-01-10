using ACIC.AMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACIC.AMS.Repository
{
    public class ACICDBContext : DbContext
    {
        public ACICDBContext(DbContextOptions<ACICDBContext> options)
              : base(options)
        {
            Database.EnsureCreated();
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<Carrier> Carrier { get; set; }
        public virtual DbSet<Claim> Claim { get; set; }
        public virtual DbSet<Commission> Commission { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<DdAccountSource> DdAccountSource { get; set; }
        public virtual DbSet<DdAccountStatus> DdAccountStatus { get; set; }
        public virtual DbSet<DdAccountType> DdAccountType { get; set; }
        public virtual DbSet<DdAccountOperationRadius> DdAccountOperationRadius { get; set; }
        public virtual DbSet<DdAccountsOperationType> DdAccountsOperationType { get; set; }
        public virtual DbSet<DdContactsTitle> DdContactsTitle { get; set; }
        public virtual DbSet<DdCoverageType> DdCoverageType { get; set; }
        public virtual DbSet<DdUsstate> DdUsstate { get; set; }
        public virtual DbSet<DdVehicleMake> DdVehicleMake { get; set; }
        public virtual DbSet<DdVehicleType> DdVehicleType { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Endorsement> Endorsement { get; set; }
        public virtual DbSet<EndtEstimate> EndtEstimate { get; set; }
   
        public virtual DbSet<Mga> Mga { get; set; }
        public virtual DbSet<Policy> Policy { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<AccountDriver> AccountDriver { get; set; }

    }
}
