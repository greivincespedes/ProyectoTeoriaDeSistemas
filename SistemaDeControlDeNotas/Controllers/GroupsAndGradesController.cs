using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeControlDeNotas.Helpers;
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

        [HttpPost]
        public ActionResult AddGroup(GroupIndexModel groupIndexModel)
        {
            if (ModelState.IsValid)
            {
                groupIndexModel.NewGroup.GroupStatus = 1;
                groupIndexModel.NewGroup.GroupCapacity = 10;

                #region Local variables

                string sNombreTabla, sNombreSP, sMsjError = string.Empty;

                DatabaseModel dbModel = new DatabaseModel();
                DatabaseHelpers dbHelper = new DatabaseHelpers();

                #endregion

                dbHelper.GenerarDataTableParametros(ref dbModel);

                DataRow dr1 = dbModel.dtParametros.NewRow();
                dr1["Nombre"] = "@groupName";
                dr1["TipoDato"] = "4";
                dr1["Valor"] = groupIndexModel.NewGroup.GroupName;

                DataRow dr2 = dbModel.dtParametros.NewRow();
                dr2["Nombre"] = "@groupCapacity";
                dr2["TipoDato"] = "9";
                dr2["Valor"] = groupIndexModel.NewGroup.GroupCapacity;

                DataRow dr3 = dbModel.dtParametros.NewRow();
                dr3["Nombre"] = "@groupStatus";
                dr3["TipoDato"] = "9";
                dr3["Valor"] = groupIndexModel.NewGroup.GroupStatus;

                dbModel.dtParametros.Rows.Add(dr1);
                dbModel.dtParametros.Rows.Add(dr2);
                dbModel.dtParametros.Rows.Add(dr3);

                sNombreTabla = "T_GRUPO";
                sNombreSP = "spCreateGroup";

                dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

                if (dbModel.sMsjError != string.Empty)
                {
                    groupIndexModel.OperationResult = "Se produjo un error al crear el grupo, por favor intentelo de nuevo mas tarde";
                    return View("OperationResult", groupIndexModel);
                }
                else
                {
                    groupIndexModel.OperationResult = "El grupo se ha creado de manera exitosa";
                    return View("OperationResult", groupIndexModel);
                }
            }
            else
            {
                return View(groupIndexModel);
            }
        }
    }
}