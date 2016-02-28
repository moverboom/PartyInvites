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
using System.Data.Entity;
using System.Web.Mvc;
using PartyInvites.WebUI.Models;
using PartyInvites.WebUI.HtmlHelpers;
using PartyInvites.WebUI.Controllers;

namespace PartyInvites.Tests {
    [TestClass]
    public class Unittest1 {

        [TestMethod]
        public void Can_Generate_Page_Links() {
            //arrange
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo {
                CurrentPage = 2,
                TotalResponses = 28,
                ResponsesPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a><a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a><a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());
        }

        [TestMethod]
        public void Can_Paginate() {
            //arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllResponses()).Returns(new List<GuestResponse> {
                new GuestResponse {
                    Name = "Matthijs",
                    Email = "matthijs@test.nl",
                    Phone = "0612345678",
                    WillAttend = true
                },
                new GuestResponse {
                    Name = "Piet",
                    Email = "piet@test.nl",
                    Phone = "0612345678",
                    WillAttend = false
                },
                new GuestResponse {
                    Name = "Truus",
                    Email = "truus@test.nl",
                    Phone = "0612345678",
                    WillAttend = true
                },
                new GuestResponse {
                    Name = "Anja",
                    Email = "anja@test.nl",
                    Phone = "0612345678",
                    WillAttend = false
                }
            });

            ResponseController target = new ResponseController(mock.Object);
            target.PageSize = 3;

            //act
            ResponseListViewModel result = (ResponseListViewModel)target.Responses(null, 2).Model;

            //assert
            GuestResponse[] responseArray = result.GuestResponses.ToArray();
            Assert.IsTrue(responseArray.Length == 1);
            Assert.AreEqual(responseArray[0].Name, "Anja");
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model() {
            //arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllResponses()).Returns(new List<GuestResponse> {
                new GuestResponse {
                    Name = "Matthijs",
                    Email = "matthijs@test.nl",
                    Phone = "0612345678",
                    WillAttend = true
                },
                new GuestResponse {
                    Name = "Piet",
                    Email = "piet@test.nl",
                    Phone = "0612345678",
                    WillAttend = false
                },
                new GuestResponse {
                    Name = "Truus",
                    Email = "truus@test.nl",
                    Phone = "0612345678",
                    WillAttend = true
                },
                new GuestResponse {
                    Name = "Anja",
                    Email = "anja@test.nl",
                    Phone = "0612345678",
                    WillAttend = false
                }
            });

            ResponseController target = new ResponseController(mock.Object);
            target.PageSize = 3;

            //act
            ResponseListViewModel result = (ResponseListViewModel)target.Responses(null, 2).Model;

            //assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ResponsesPerPage, 3);
            Assert.AreEqual(pageInfo.TotalResponses, 4);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Responses_All() {
            //arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllResponses()).Returns(new List<GuestResponse> {
                new GuestResponse {
                    Name = "Matthijs",
                    Email = "matthijs@test.nl",
                    Phone = "0612345678",
                    WillAttend = true
                },
                new GuestResponse {
                    Name = "Piet",
                    Email = "piet@test.nl",
                    Phone = "0612345678",
                    WillAttend = false
                },
                new GuestResponse {
                    Name = "Truus",
                    Email = "truus@test.nl",
                    Phone = "0612345678",
                    WillAttend = true
                },
                new GuestResponse {
                    Name = "Anja",
                    Email = "anja@test.nl",
                    Phone = "0612345678",
                    WillAttend = false
                }
            });

            ResponseController target = new ResponseController(mock.Object);
            target.PageSize = 3;

            //act
            GuestResponse[] result = ((ResponseListViewModel)target.Responses("All").Model).GuestResponses.ToArray();


            //assert
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result[0].Name == "Matthijs" && result[0].WillAttend == true);
            Assert.IsTrue(result[2].Name == "Truus" && result[1].WillAttend == false);
        }

        [TestMethod]
        public void Can_Filter_Responses_Not_Attending() {
            //arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllResponses()).Returns(new List<GuestResponse> {
                new GuestResponse {
                    Name = "Matthijs",
                    Email = "matthijs@test.nl",
                    Phone = "0612345678",
                    WillAttend = true
                },
                new GuestResponse {
                    Name = "Piet",
                    Email = "piet@test.nl",
                    Phone = "0612345678",
                    WillAttend = false
                },
                new GuestResponse {
                    Name = "Truus",
                    Email = "truus@test.nl",
                    Phone = "0612345678",
                    WillAttend = true
                },
                new GuestResponse {
                    Name = "Anja",
                    Email = "anja@test.nl",
                    Phone = "0612345678",
                    WillAttend = false
                }
            });

            ResponseController target = new ResponseController(mock.Object);
            target.PageSize = 3;

            //act
            GuestResponse[] result = ((ResponseListViewModel)target.Responses("Will Not Attend").Model).GuestResponses.ToArray();


            //assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Piet" && result[0].WillAttend == false);
            Assert.IsTrue(result[1].Name == "Anja" && result[1].WillAttend == false);
        }

        [TestMethod]
        public void Can_Filter_Responses_Attending() {
            //arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllResponses()).Returns(new List<GuestResponse> {
                new GuestResponse {
                    Name = "Matthijs",
                    Email = "matthijs@test.nl",
                    Phone = "0612345678",
                    WillAttend = true
                },
                new GuestResponse {
                    Name = "Piet",
                    Email = "piet@test.nl",
                    Phone = "0612345678",
                    WillAttend = false
                },
                new GuestResponse {
                    Name = "Truus",
                    Email = "truus@test.nl",
                    Phone = "0612345678",
                    WillAttend = true
                },
                new GuestResponse {
                    Name = "Anja",
                    Email = "anja@test.nl",
                    Phone = "0612345678",
                    WillAttend = false
                }
            });

            ResponseController target = new ResponseController(mock.Object);
            target.PageSize = 3;

            //act
            GuestResponse[] result = ((ResponseListViewModel)target.Responses("Will Attend").Model).GuestResponses.ToArray();


            //assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Matthijs" && result[0].WillAttend == true);
            Assert.IsTrue(result[1].Name == "Truus" && result[1].WillAttend == true);
        }
    }
}
