using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyInvites.Domain.Entities;
using PartyInvites.Domain.Abstract;

namespace PartyInvites.Domain.Concrete {
    public class EFGuestReponseRepository : IRepository {
        private EFDbContext context = new EFDbContext();

        public GuestResponse GetResponse(string email) {
            throw new NotImplementedException();
        }

        public IEnumerable<GuestResponse> GetAllResponses() {
            return context.GuestResponses;
        }

        public bool AddResponse(GuestResponse response) {
            throw new NotImplementedException();
        }

        public bool EditResponse(GuestResponse response) {
            throw new NotImplementedException();
        }
    }
}
