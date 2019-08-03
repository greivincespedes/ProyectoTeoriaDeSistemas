using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeControlDeNotas.Models;

namespace SistemaDeControlDeNotas.Controllers
{
    public class CategoryMaintenanceController : Controller
    {
        [HttpGet]
        public ActionResult Index(MemberUserModel user)
        {
            return View(user);
        }

        [HttpGet]
        public ActionResult Back(MemberUserModel user)
        {
            return View("~/Views/Login/Home.cshtml", user);
        }

        [HttpGet]
        public ActionResult AddCategory(MemberUserModel user)
        {
            CategoryIndexModel categoryIndexModel = new CategoryIndexModel();
            categoryIndexModel.CurrentUser = user;

            return View(categoryIndexModel);
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryIndexModel categoryIndexModel)
        {
            if (ModelState.IsValid)
            {
                return View(categoryIndexModel);
            }
            else
            {
                return View(categoryIndexModel);
            }
        }
    }
}