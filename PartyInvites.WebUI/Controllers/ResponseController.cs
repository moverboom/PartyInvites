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
    public class ResponseController : Controller
    {
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
        public ViewResult Rsvp(GuestResponse guestResponse) {
            if (ModelState.IsValid) {
                ViewBag.ResponseAdded = repository.AddResponse(guestResponse);
                return View("Thanks", guestResponse);
            } else {
                return View();
            }
        }

        public ViewResult Responses(int page = 1) {
            ResponseListViewModel model = new ResponseListViewModel {
                GuestResponses = repository.GetAllResponses()
                                    .OrderBy(g => g.GuestResponseID)
                                    .Skip((page - 1) * PageSize)
                                    .Take(PageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ResponsesPerPage = PageSize,
                    TotalResponses = repository.GetAllResponses().Count()
                }
            };
            return View(model);
        }

        public PartialViewResult ResponseSummary() {
            return PartialView(repository.GetAllResponses().ToArray());
        }
    }
}