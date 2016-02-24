using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyInvites.Domain.Abstract;

namespace PartyInvites.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;

        public HomeController(IRepository repositoryParam) {
            repository = repositoryParam;
        }

       public ViewResult Index() {
            return View();
       }
    }
}