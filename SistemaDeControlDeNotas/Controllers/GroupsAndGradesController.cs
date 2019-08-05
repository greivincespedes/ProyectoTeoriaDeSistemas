using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeControlDeNotas.Models;

namespace SistemaDeControlDeNotas.Controllers
{
    public class GroupsAndGradesController : Controller
    {
        [HttpGet]
        public ActionResult Index(MemberUserModel memberUserModel)
        {
            return View(memberUserModel);
        }

        [HttpGet]
        public ActionResult Back(MemberUserModel user)
        {
            return View("~/Views/Login/Home.cshtml", user);
        }

        [HttpGet]
        public ActionResult AddGroup(MemberUserModel memberUserModel)
        {
            GroupIndexModel groupIndexModel = new GroupIndexModel();
            groupIndexModel.CurrentUser = memberUserModel;

            return View(groupIndexModel);
        }
    }
}