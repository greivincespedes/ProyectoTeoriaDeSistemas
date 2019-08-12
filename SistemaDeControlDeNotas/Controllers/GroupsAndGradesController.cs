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

        [HttpGet]
        public ActionResult AssignWork(MemberUserModel user)
        {
            WorkIndexModel workIndexModel = new WorkIndexModel();
            workIndexModel.CurrentUser = user;

            workIndexModel.GroupsList = new SelectGroupHelpers();

            return View(workIndexModel);
        }

        [HttpPost]
        public ActionResult AssignWork(WorkIndexModel workIndexModel)
        {
            if (ModelState.IsValid)
            {
                workIndexModel.NewWork.WorkStatus = 1;

                #region Local variables

                string sNombreTabla, sNombreSP, sMsjError = string.Empty;

                DatabaseModel dbModel = new DatabaseModel();
                DatabaseHelpers dbHelper = new DatabaseHelpers();

                #endregion

                dbHelper.GenerarDataTableParametros(ref dbModel);

                DataRow dr1 = dbModel.dtParametros.NewRow();
                dr1["Nombre"] = "@grupo";
                dr1["TipoDato"] = "9";
                dr1["Valor"] = workIndexModel.NewWork.GroupID;

                DataRow dr2 = dbModel.dtParametros.NewRow();
                dr2["Nombre"] = "@asunto";
                dr2["TipoDato"] = "4";
                dr2["Valor"] = workIndexModel.NewWork.Subject;

                DataRow dr3 = dbModel.dtParametros.NewRow();
                dr3["Nombre"] = "@descripcion";
                dr3["TipoDato"] = "4";
                dr3["Valor"] = workIndexModel.NewWork.Description;

                DataRow dr4 = dbModel.dtParametros.NewRow();
                dr4["Nombre"] = "@estado";
                dr4["TipoDato"] = "9";
                dr4["Valor"] = workIndexModel.NewWork.WorkStatus;

                dbModel.dtParametros.Rows.Add(dr1);
                dbModel.dtParametros.Rows.Add(dr2);
                dbModel.dtParametros.Rows.Add(dr3);
                dbModel.dtParametros.Rows.Add(dr4);

                sNombreTabla = "T_TRABAJO";
                sNombreSP = "spCrearTrabajo";

                dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

                if (dbModel.sMsjError != string.Empty)
                {
                    workIndexModel.OperationResult = "Se produjo un error al crear el trabajo, por favor intentelo de nuevo mas tarde";

                    GroupIndexModel groupIndexModel = new GroupIndexModel();
                    groupIndexModel.CurrentUser = workIndexModel.CurrentUser;
                    groupIndexModel.OperationResult = workIndexModel.OperationResult;
                    
                    return View("OperationResult", groupIndexModel);
                }
                else
                {
                    workIndexModel.OperationResult = "El trabajo se ha creado de manera exitosa";

                    GroupIndexModel groupIndexModel = new GroupIndexModel();
                    groupIndexModel.CurrentUser = workIndexModel.CurrentUser;
                    groupIndexModel.OperationResult = workIndexModel.OperationResult;
                    
                    return View("OperationResult", groupIndexModel);
                }
            }
            else
            {
                return View(workIndexModel);
            }
        }

        [HttpGet]
        public ActionResult AssignTask(TaskIndexModel taskIndexModel)
        {
            taskIndexModel.StudentList = new SelectStudentHelpers(taskIndexModel.NewTask.GroupID);
            return View(taskIndexModel);
        }

        [HttpPost]
        public ActionResult CreateTask(TaskIndexModel taskIndexModel)
        {
            if (ModelState.IsValid)
            {
                taskIndexModel.NewTask.TaskStatus = 1;

                #region Local variables

                string sNombreTabla, sNombreSP, sMsjError = string.Empty;

                DatabaseModel dbModel = new DatabaseModel();
                DatabaseHelpers dbHelper = new DatabaseHelpers();

                #endregion

                dbHelper.GenerarDataTableParametros(ref dbModel);

                DataRow dr1 = dbModel.dtParametros.NewRow();
                dr1["Nombre"] = "@work";
                dr1["TipoDato"] = "1";
                dr1["Valor"] = taskIndexModel.NewTask.WorkID;

                DataRow dr2 = dbModel.dtParametros.NewRow();
                dr2["Nombre"] = "@asunto";
                dr2["TipoDato"] = "4";
                dr2["Valor"] = taskIndexModel.NewTask.Subject;

                DataRow dr3 = dbModel.dtParametros.NewRow();
                dr3["Nombre"] = "@descripcion";
                dr3["TipoDato"] = "4";
                dr3["Valor"] = taskIndexModel.NewTask.Description;

                DataRow dr4 = dbModel.dtParametros.NewRow();
                dr4["Nombre"] = "@estado";
                dr4["TipoDato"] = "9";
                dr4["Valor"] = taskIndexModel.NewTask.TaskStatus;

                DataRow dr5 = dbModel.dtParametros.NewRow();
                dr5["Nombre"] = "@usuario";
                dr5["TipoDato"] = "1";
                dr5["Valor"] = taskIndexModel.NewTask.StudentID;

                dbModel.dtParametros.Rows.Add(dr1);
                dbModel.dtParametros.Rows.Add(dr2);
                dbModel.dtParametros.Rows.Add(dr3);
                dbModel.dtParametros.Rows.Add(dr4);
                dbModel.dtParametros.Rows.Add(dr5);

                sNombreTabla = "T_TRABAJO";
                sNombreSP = "spCrearTarea";

                dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

                if (dbModel.sMsjError != string.Empty)
                {
                    taskIndexModel.OperationResult = "Se produjo un error al crear la tarea, por favor intentelo de nuevo mas tarde";

                    GroupIndexModel groupIndexModel = new GroupIndexModel();
                    groupIndexModel.CurrentUser = taskIndexModel.CurrentUser;
                    groupIndexModel.OperationResult = taskIndexModel.OperationResult;

                    return View("OperationResult", groupIndexModel);
                }
                else
                {
                    taskIndexModel.OperationResult = "La tarea se ha creado de manera exitosa";

                    GroupIndexModel groupIndexModel = new GroupIndexModel();
                    groupIndexModel.CurrentUser = taskIndexModel.CurrentUser;
                    groupIndexModel.OperationResult = taskIndexModel.OperationResult;

                    return View("OperationResult", groupIndexModel);
                }
            }
            else
            {
                return View(taskIndexModel);
            }
        }

        [HttpGet]
        public ActionResult SelectGroupForTask(MemberUserModel user)
        {
            TaskIndexModel taskIndexModel = new TaskIndexModel();
            taskIndexModel.CurrentUser = user;

            taskIndexModel.GroupsList = new SelectGroupHelpers();

            ViewBag.HtmlStr = "action=\"SelectWorkForTask\"";

            return View("SelectGroup", taskIndexModel);
        }

        [HttpGet]
        public ActionResult SelectWorkForTask(TaskIndexModel taskIndexModel)
        {
            taskIndexModel.WorksList = new SelectWorkHelpers(taskIndexModel.NewTask.GroupID);

            ViewBag.HtmlStr = "action=\"AssignTask\"";

            return View("SelectWork", taskIndexModel);
        }

        [HttpGet]
        public ActionResult SelectGroupForEvaluation(MemberUserModel user)
        {
            TaskIndexModel taskIndexModel = new TaskIndexModel();
            taskIndexModel.CurrentUser = user;

            taskIndexModel.GroupsList = new SelectGroupHelpers();

            ViewBag.HtmlStr = "action=\"SelectWorkForEvaluation\"";

            return View("SelectGroup", taskIndexModel);
        }

        [HttpGet]
        public ActionResult SelectWorkForEvaluation(TaskIndexModel taskIndexModel)
        {
            taskIndexModel.WorksList = new SelectWorkHelpers(taskIndexModel.NewTask.GroupID);

            ViewBag.HtmlStr = "action=\"SelectStudentForEvaluation\"";

            return View("SelectWork", taskIndexModel);
        }

        [HttpGet]
        public ActionResult SelectStudentForEvaluation(TaskIndexModel taskIndexModel)
        {
            taskIndexModel.StudentList = new SelectStudentHelpers(taskIndexModel.NewTask.GroupID);

            ViewBag.HtmlStr = "action=\"EvaluateStudent\"";

            return View("SelectStudent", taskIndexModel);
        }

        [HttpGet]
        public ActionResult EvaluateStudent(TaskIndexModel taskIndexModel)
        {
            #region Local variables

            string sNombreTabla, sNombreSP, sMsjError = string.Empty;

            DatabaseModel dbModel = new DatabaseModel();
            DatabaseHelpers dbHelper = new DatabaseHelpers();

            #endregion

            EvaluationIndexModel evaluationIndexModel = new EvaluationIndexModel();

            evaluationIndexModel.Evaluation = new EvaluationModel();

            evaluationIndexModel.CurrentUser = taskIndexModel.CurrentUser;
            evaluationIndexModel.Evaluation.GroupID = taskIndexModel.NewTask.GroupID;
            evaluationIndexModel.Evaluation.WorkID = taskIndexModel.NewTask.WorkID;
            evaluationIndexModel.Evaluation.StudentID = taskIndexModel.NewTask.StudentID;

            dbHelper.GenerarDataTableParametros(ref dbModel);

            DataRow dr1 = dbModel.dtParametros.NewRow();
            dr1["Nombre"] = "@estudiante";
            dr1["TipoDato"] = "10";
            dr1["Valor"] = evaluationIndexModel.Evaluation.StudentID;

            DataRow dr2 = dbModel.dtParametros.NewRow();
            dr2["Nombre"] = "@grupo";
            dr2["TipoDato"] = "9";
            dr2["Valor"] = evaluationIndexModel.Evaluation.GroupID;

            DataRow dr3 = dbModel.dtParametros.NewRow();
            dr3["Nombre"] = "@trabajo";
            dr3["TipoDato"] = "1";
            dr3["Valor"] = evaluationIndexModel.Evaluation.WorkID;

            dbModel.dtParametros.Rows.Add(dr1);
            dbModel.dtParametros.Rows.Add(dr2);
            dbModel.dtParametros.Rows.Add(dr3);

            sNombreTabla = "T_USUARIO";
            sNombreSP = "spListarDetallesParaEvaluacion";

            dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

            if (dbModel.sMsjError != string.Empty)
            {
                evaluationIndexModel.OperationResult = "Se produjo un error al cargar el formularion, por favor intentelo de nuevo mas tarde";

                GroupIndexModel groupIndexModel = new GroupIndexModel();
                groupIndexModel.CurrentUser = evaluationIndexModel.CurrentUser;
                groupIndexModel.OperationResult = evaluationIndexModel.OperationResult;

                return View("OperationResult", groupIndexModel);
            }
            else
            {
                evaluationIndexModel.OperationResult = "El trabajo se ha creado de manera exitosa";

                evaluationIndexModel.TasksList = new SelectTaskHelpers(evaluationIndexModel.Evaluation.StudentID);

                for (int i = 0; i < dbModel.DS.Tables[sNombreTabla].Rows.Count; i++)
                {
                    evaluationIndexModel.Evaluation.StudentName = dbModel.DS.Tables[sNombreTabla].Rows[i][0].ToString();
                    evaluationIndexModel.Evaluation.GroupName = dbModel.DS.Tables[sNombreTabla].Rows[i][1].ToString();
                    evaluationIndexModel.Evaluation.WorkSubject = dbModel.DS.Tables[sNombreTabla].Rows[i][2].ToString();
                }

                return View(evaluationIndexModel);
            }

            
        }

        [HttpPost]
        public ActionResult EvaluateStudent(EvaluationIndexModel evaluationIndexModel)
        {
            #region Local variables

            string sNombreTabla, sNombreSP, sMsjError = string.Empty;

            DatabaseModel dbModel = new DatabaseModel();
            DatabaseHelpers dbHelper = new DatabaseHelpers();

            #endregion

            dbHelper.GenerarDataTableParametros(ref dbModel);

            DataRow dr1 = dbModel.dtParametros.NewRow();
            dr1["Nombre"] = "@estudiante";
            dr1["TipoDato"] = "10";
            dr1["Valor"] = evaluationIndexModel.Evaluation.StudentID;

            DataRow dr2 = dbModel.dtParametros.NewRow();
            dr2["Nombre"] = "@tarea";
            dr2["TipoDato"] = "1";
            dr2["Valor"] = evaluationIndexModel.Evaluation.TaskID;

            DataRow dr3 = dbModel.dtParametros.NewRow();
            dr3["Nombre"] = "@escalaLiderazgo";
            dr3["TipoDato"] = "5";
            dr3["Valor"] = evaluationIndexModel.Evaluation.LeadershipScale;

            DataRow dr4 = dbModel.dtParametros.NewRow();
            dr4["Nombre"] = "@escalaPuntualidad";
            dr4["TipoDato"] = "5";
            dr4["Valor"] = evaluationIndexModel.Evaluation.PunctualityScale;

            DataRow dr5 = dbModel.dtParametros.NewRow();
            dr5["Nombre"] = "@escalaHonestidad";
            dr5["TipoDato"] = "5";
            dr5["Valor"] = evaluationIndexModel.Evaluation.HonestyScale;

            DataRow dr6 = dbModel.dtParametros.NewRow();
            dr6["Nombre"] = "@escalaActitud";
            dr6["TipoDato"] = "5";
            dr6["Valor"] = evaluationIndexModel.Evaluation.AttitudeScale;

            DataRow dr7 = dbModel.dtParametros.NewRow();
            dr7["Nombre"] = "@estado";
            dr3["TipoDato"] = "9";
            dr7["Valor"] = 1;

            DataRow dr8 = dbModel.dtParametros.NewRow();

            dbModel.dtParametros.Rows.Add(dr1);
            dbModel.dtParametros.Rows.Add(dr2);
            dbModel.dtParametros.Rows.Add(dr3);
            dbModel.dtParametros.Rows.Add(dr4);
            dbModel.dtParametros.Rows.Add(dr5);
            dbModel.dtParametros.Rows.Add(dr6);
            dbModel.dtParametros.Rows.Add(dr7);

            sNombreTabla = "T_EVALUACION";
            sNombreSP = "spCrearEvaluacion";

            dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

            if (dbModel.sMsjError != string.Empty)
            {
                evaluationIndexModel.OperationResult = "Se produjo un error al enviar la evaluacion, por favor intentelo de nuevo mas tarde";

                GroupIndexModel groupIndexModel = new GroupIndexModel();
                groupIndexModel.CurrentUser = evaluationIndexModel.CurrentUser;
                groupIndexModel.OperationResult = evaluationIndexModel.OperationResult;

                return View("OperationResult", groupIndexModel);
            }
            else
            {
                evaluationIndexModel.OperationResult = "La evaluacion se ha enviado de manera exitosa";

                GroupIndexModel groupIndexModel = new GroupIndexModel();
                groupIndexModel.CurrentUser = evaluationIndexModel.CurrentUser;
                groupIndexModel.OperationResult = evaluationIndexModel.OperationResult;

                return View("OperationResult", groupIndexModel);
            }
        }
    }
}