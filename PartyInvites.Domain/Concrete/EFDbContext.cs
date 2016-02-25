using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PartyInvites.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PartyInvites.Domain.Concrete {
    public class EFDbContext : DbContext {

        public EFDbContext() : base("EFDbContext") {

        }

        //Mapped GuestResponse class to GuestResponses database table
        public DbSet<GuestResponse> GuestResponses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            //Disabled pluralized table names
            //I prefer the singular form for table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
