using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PartyInvites.Domain.Entities;

namespace PartyInvites.Domain.Concrete {
    public class EFDbContext : DbContext {
        //Mapped GuestResponse class to GuestResponses database table
        public DbSet<GuestResponse> GuestResponses { get; set; }
    }
}
