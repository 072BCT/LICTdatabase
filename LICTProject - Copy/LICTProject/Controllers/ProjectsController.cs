using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LICTProject.Models;
using Newtonsoft.Json;
using System.IO;

namespace LICTProject.Controllers
{
    public class ProjectsController : Controller
    {
        private Entities db = new Entities();

       

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }
        public ActionResult UserView()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "FullName");        
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,Project_Title,StartTime,EndTime,Duration,Describe,GrantProvider,GrantAmt,UniqueId")] Project project)
        {
            if (ModelState.IsValid)
            {               
                db.Projects.Add(project);
                db.SaveChanges();
                //var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Defaultconnection"].ConnectionString);
                //using (connection)
                //{
                //    SqlDataReader dr;
                //    connection.Open();
                //    string query = "SELECT * FROM dbo.TempData";
                //    SqlCommand com = new SqlCommand(query, connection);
                //    SqlCommand cmd = new SqlCommand("InsertData", connection);
                //    cmd.CommandType = CommandType.StoredProcedure;

                //    com.ExecuteNonQuery();
                //    dr = com.ExecuteReader();
                //    while (dr.Read())
                //    {
                //        cmd.Parameters.AddWithValue("@ProjectID", dr[1].ToString());
                //        cmd.Parameters.AddWithValue("@PersonnelID", dr[2].ToString());
                //        cmd.ExecuteNonQuery();
                //    }
                //    dr.Close();

                //}
                //connection.Close();
                ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "FullName");

                //return View();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "FullName");
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,Project_Title,StartTime,EndTime,Duration,Describe,GrantProvider,GrantAmt")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();

               
                return RedirectToAction("Index");
            }
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "FullName");
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        [HttpPost]
        public JsonResult SetProjectMember(string ProjectMemberJSON)
        {
            // ListMember members = JsonConvert.DeserializeObject<ListMember>(ProjectMemberJSON.ToString());
            Members[] members = JsonConvert.DeserializeObject<Members[]>(ProjectMemberJSON.ToString());
            
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Defaultconnection"].ConnectionString);
            using (connection)
            {
                SqlDataReader dr;
                connection.Open();
                string query = "SELECT TOP 1 * FROM dbo.Project ORDER BY ProjectID DESC";
                SqlCommand com = new SqlCommand(query, connection);
                //com.Parameters.AddWithValue("@UniqueId", members[0].ProjectID);
                com.ExecuteNonQuery();
                dr = com.ExecuteReader();
                int ProjectID=0;
                while (dr.Read())
                {
                    if (dr[8].ToString().Equals(members[0].ProjectID))
                    {
                        ProjectID = Convert.ToInt32(dr[0]);                       
                    }
                    else
                    {
                        ProjectID = Convert.ToInt32(dr[0])+1;
                    }
                   
                                    
                }
                dr.Close();
               

                SqlCommand cmd = new SqlCommand("InsertData", connection);
                cmd.CommandType = CommandType.StoredProcedure;              
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@PersonnelID", members[0].PersonnelID);
               
                cmd.ExecuteNonQuery();
              
            }
            connection.Close();
            return Json("Success");
        }

        [HttpPost]
        public JsonResult SetOtherMembers(string OtherMemberJSON)
        {
            // ListMember members = JsonConvert.DeserializeObject<ListMember>(ProjectMemberJSON.ToString());
            OtherMembers[] members = JsonConvert.DeserializeObject<OtherMembers[]>(OtherMemberJSON.ToString());

            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Defaultconnection"].ConnectionString);
            using (connection)
            {
                SqlDataReader dr;
                connection.Open();
                string query = "SELECT TOP 1 * FROM dbo.Project ORDER BY ProjectID DESC";
                SqlCommand com = new SqlCommand(query, connection);
                com.ExecuteNonQuery();
                dr = com.ExecuteReader();
                int ProjectID = 0;
                while (dr.Read())
                {
                    if (dr[8].ToString().Equals(members[0].ProjectID))
                    {
                        ProjectID = Convert.ToInt32(dr[0]);
                    }
                    else
                    {
                        ProjectID = Convert.ToInt32(dr[0])+1;
                    }

                }
                dr.Close();
               
                SqlCommand cmd = new SqlCommand("InsertOtherMember", connection);
                cmd.CommandType = CommandType.StoredProcedure;
               
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@OtherMember", members[0].OtherMember);
               
                cmd.ExecuteNonQuery();
            }
            connection.Close();
            return Json("Success");
        }



        //for uniqueID validation// doesn't work i guess

        [HttpPost]
        public JsonResult DoesAlreadyExists(string UniqueID)
        {

            return Json(IsIdAvailable(UniqueID));

        }
        public bool IsIdAvailable(string UniqueID)
        {  
            List<Project> projects = new List<Project>();
            var ID = (from u in projects
                              where u.UniqueId.ToUpper() == UniqueID.ToUpper()
                              select new { UniqueID }).FirstOrDefault();

            bool status;
            if (ID != null)
            {

                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }


            return status;
        }

    }
}
