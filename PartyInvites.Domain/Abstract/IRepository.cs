using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyInvites.Domain.Entities;

namespace PartyInvites.Domain.Abstract {
    public interface IRepository {
        GuestResponse GetResponse(string email);

        IEnumerable<GuestResponse> GetAllResponses();

        bool AddResponse(GuestResponse response);

        bool EditResponse(GuestResponse response);
    }
}
