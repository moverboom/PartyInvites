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
    [TestClass]
    public class RepositoryTest {

        [TestMethod]
        public void Can_Add_Response() {
            //arrange
            IRepository repository = getTestRepository();
            GuestResponse response = new GuestResponse {
                Name = "Matthijs Overboom",
                Email = getRandomEmail(),
                Phone = "0637425784",
                WillAttend = false
            };

            bool result = repository.AddResponse(response);
            int rows = repository.GetAllResponses().Count();

            //assert act
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Can_Not_Add_Response_Existing() {
            //arrange
            IRepository repository = getTestRepository();
            GuestResponse response = new GuestResponse {
                Name = "Matthijs Overboom",
                Email = getRandomEmail(),
                Phone = "0637425784",
                WillAttend = false
            };

            //act
            repository.AddResponse(response);

            //assert act
            Assert.IsFalse(repository.AddResponse(response));
        }

        [TestMethod]
        public void Can_Get_Responses() {
            //arrange
            IRepository repository = getTestRepository();
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
            Assert.AreEqual(response.Email, result.Email);
        }
  

        private IRepository getTestRepository() {
            return new EFGuestReponseRepository();
        }


        //generate random email to prevent test failures caused by a duplicate email
        private string getRandomEmail() {
            return new Random().Next(1, 99999)+"@test.nl";
        }
    }
}
