using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartyInvites.Domain.Entities;
using PartyInvites.Domain.Abstract;
using PartyInvites.Domain.Concrete;
using Moq;

namespace PartyInvites.Tests {
    public class RepositoryTest {
        [TestMethod]
        public void Can_Get_Responses() {
            //arrange
            EFGuestReponseRepository repository = new EFGuestReponseRepository();
            GuestResponse response = new GuestResponse {
                Name = "Matthijs",
                Email = "test@test.nl",
                Phone = "0612345678",
                WillAttend = true
            };

            repository.AddResponse(response);

            //act
            GuestResponse result = repository.GetResponse(response.Email);

            //assert
            Assert.AreEqual(result, response);
        }

        [TestMethod]
        public void Can_Add_Response() {
            //arrange
            EFGuestReponseRepository repository = new EFGuestReponseRepository();
            GuestResponse response = new GuestResponse {
                Name = "Matthijs",
                Email = "test@test.nl",
                Phone = "0612345678",
                WillAttend = true
            };

            //act
            repository.AddResponse(response);

            //assert
            Assert.AreEqual(repository.GetResponse(response.Email), response);
        }
    }
}
