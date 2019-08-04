using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeControlDeNotas.Models;
using SistemaDeControlDeNotas.Helpers;

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
                #region Local variables

                string sNombreTabla, sNombreSP, sMsjError = string.Empty;

                DatabaseModel dbModel = new DatabaseModel();
                DatabaseHelpers dbHelper = new DatabaseHelpers();

                #endregion

                dbHelper.GenerarDataTableParametros(ref dbModel);

                DataRow dr1 = dbModel.dtParametros.NewRow();
                dr1["Nombre"] = "@nombreCategoria";
                dr1["TipoDato"] = "4";
                dr1["Valor"] = categoryIndexModel.Category.Name;

                DataRow dr2 = dbModel.dtParametros.NewRow();
                dr2["Nombre"] = "@descripcionCategoria";
                dr2["TipoDato"] = "4";
                dr2["Valor"] = categoryIndexModel.Category.Description;

                DataRow dr3 = dbModel.dtParametros.NewRow();
                dr3["Nombre"] = "@estadoCategoria";
                dr3["TipoDato"] = "9";
                dr3["Valor"] = categoryIndexModel.Category.State;

                dbModel.dtParametros.Rows.Add(dr1);
                dbModel.dtParametros.Rows.Add(dr2);
                dbModel.dtParametros.Rows.Add(dr3);

                sNombreTabla = "T_CATEGORIA";
                sNombreSP = "spCrearCategoria";

                dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

                if (dbModel.sMsjError != string.Empty)
                {
                    categoryIndexModel.OperationResult = "Se produjo un error al crear la categoria, por favor intentelo de nuevo mas tarde";
                    return View("OperationResult", categoryIndexModel); ;
                }
                else
                {
                    categoryIndexModel.OperationResult = "La categoria se ha creado de manera exitosa";
                    return View("OperationResult", categoryIndexModel); ;
                }
            }
            else
            {
                return View(categoryIndexModel);
            }
        }

        [HttpGet]
        public ActionResult SelectCategoryToModify(MemberUserModel user)
        {
            ModifyCategoryIndexModel modifyCategoryIndexModel = new ModifyCategoryIndexModel();
            modifyCategoryIndexModel.CurrentUser = user;

            modifyCategoryIndexModel.CategoriesList = new SelectCategoryHelpers();

            ViewBag.HtmlStr = "action=\"ModifyCategory\"";

            return View("SelectCategory", modifyCategoryIndexModel);
        }

        [HttpGet]
        public ActionResult ModifyCategory(ModifyCategoryIndexModel modifyCategoryIndexModel)
        {
            #region Local variables

            string sNombreTabla, sNombreSP;

            DatabaseModel dbModel = new DatabaseModel();
            DatabaseHelpers dbHelper = new DatabaseHelpers();

            #endregion

            dbHelper.GenerarDataTableParametros(ref dbModel);

            DataRow dr1 = dbModel.dtParametros.NewRow();
            dr1["Nombre"] = "@categoryID";
            dr1["TipoDato"] = "9";
            dr1["Valor"] = modifyCategoryIndexModel.ModifyCategory.CategoryID;

            dbModel.dtParametros.Rows.Add(dr1);

            sNombreTabla = "T_CATEGORIA";
            sNombreSP = "spListCategory";

            dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

            if (dbModel.sMsjError != string.Empty)
            {
                modifyCategoryIndexModel.OperationResult = dbModel.sMsjError;

                return View("OperationResult", modifyCategoryIndexModel);
            }
            else
            {
                modifyCategoryIndexModel.ModifyCategory.CategoryID = Convert.ToInt32(dbModel.DS.Tables[sNombreTabla].Rows[0][0].ToString());
                modifyCategoryIndexModel.ModifyCategory.Name = dbModel.DS.Tables[sNombreTabla].Rows[0][1].ToString();
                modifyCategoryIndexModel.ModifyCategory.Description = dbModel.DS.Tables[sNombreTabla].Rows[0][2].ToString();
                modifyCategoryIndexModel.ModifyCategory.State = Convert.ToByte(dbModel.DS.Tables[sNombreTabla].Rows[0][3].ToString());

                return View(modifyCategoryIndexModel);
            }
        }
    }
}