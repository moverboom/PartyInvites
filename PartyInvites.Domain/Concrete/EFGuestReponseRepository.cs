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
            if (context.GuestResponses.Count(g => g.Email == email) == 1) {
                return context.GuestResponses.Where(g => g.Email == email) as GuestResponse;
            } else {
                return null;
            }
        }

        public IEnumerable<GuestResponse> GetAllResponses() {
            return context.GuestResponses;
        }

        public bool AddResponse(GuestResponse response) {
            if (context.GuestResponses.Count(g => g.Email == response.Email) == 0) {
                context.GuestResponses.Add(response);
                context.SaveChanges();
                return true;
            } else {
                return false;
            }
        }

        public bool EditResponse(GuestResponse response) {
            throw new NotImplementedException();
        }
    }
}
