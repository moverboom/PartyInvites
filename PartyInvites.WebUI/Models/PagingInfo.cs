using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace PartyInvites.WebUI.Models {
    public class PagingInfo {
        public int TotalResponses { get; set; }
        public int ResponsesPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages {
            get { return (int)Math.Ceiling((decimal)TotalResponses / ResponsesPerPage); }
        }
    }
}