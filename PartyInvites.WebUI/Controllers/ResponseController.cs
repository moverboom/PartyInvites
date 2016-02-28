using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using PartyInvites.Domain.Abstract;
using PartyInvites.Domain.Entities;
using PartyInvites.WebUI.HtmlHelpers;
using PartyInvites.WebUI.Models;

namespace PartyInvites.WebUI.Controllers
{
    public class ResponseController : Controller {
        private IRepository repository;
        private int PageSize = 4;

        public ResponseController(IRepository repositoryParam) {
            repository = repositoryParam;
        }

        [HttpGet]
        public ViewResult Rsvp() {
            return View();
        }

        [HttpPost]
        public ViewResult Rsvp(GuestResponse guestResponse, HttpPostedFileBase image = null) {
            if (ModelState.IsValid) {
                if (image != null) {
                    guestResponse.ImageMimeType = image.ContentType;
                    guestResponse.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(guestResponse.ImageData, 0, image.ContentLength);
                }
                ViewBag.ResponseAdded = repository.AddResponse(guestResponse);
                return View("Thanks", guestResponse);
            } else {
                return View();
            }
        }

        public FileContentResult GetImage(string Email) {
            GuestResponse guestResponse = repository.GetResponse(Email);
            if (guestResponse != null) {
                return File(guestResponse.ImageData, guestResponse.ImageMimeType);
            } else {
                return null;
            }
        }

        
        public ViewResult Responses(string filter, int page = 1) {
            int filterVal = 0;
            switch (filter) {
                case "Will Attend":
                    filterVal = 1;
                    break;
                case "Will Not Attend":
                    filterVal = 2;
                    break;
                default:
                    filterVal = 3;
                    break;
                }

            ResponseListViewModel model = new ResponseListViewModel {
                GuestResponses = repository.GetAllResponses()
                                    .Where(g => filterVal == 3 || g.WillAttend == (filterVal == 1) ? true : false)
                                    .OrderBy(g => g.GuestResponseID)
                                    .Skip((page - 1) * PageSize)
                                    .Take(PageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ResponsesPerPage = PageSize,
                    TotalResponses = filterVal == 3 ?
                                        repository.GetAllResponses().Count() :
                                        repository.GetAllResponses().Where(g => g.WillAttend == (filterVal == 1) ? true : false).Count()
                },
                Filter = filter
            };
  
            return View(model);
        }

        public PartialViewResult ResponseSummary() {
            return PartialView(repository.GetAllResponses().ToArray());
        }
    }
}