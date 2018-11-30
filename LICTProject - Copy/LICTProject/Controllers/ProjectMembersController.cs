using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LICTProject.Models;

namespace LICTProject.Controllers
{
    public class ProjectMembersController : Controller
    {
        private Entities db = new Entities();

        // GET: ProjectMembers
        public ActionResult Index()
        {
            var projectMembers = db.ProjectMembers.Include(p => p.Personnel).Include(p => p.Project);
            return View(projectMembers.ToList());
        }

        // GET: ProjectMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectMember projectMember = db.ProjectMembers.Find(id);
            if (projectMember == null)
            {
                return HttpNotFound();
            }
            return View(projectMember);
        }

        // GET: ProjectMembers/Create
        public ActionResult Create()
        {
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "FullName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "Project_Title");
            return View();
        }

        // POST: ProjectMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectMembersID,ProjectID,PersonnelID")] ProjectMember projectMember)
        {
            if (ModelState.IsValid)
            {
                db.ProjectMembers.Add(projectMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "FullName", projectMember.PersonnelID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "Project_Title", projectMember.ProjectID);
            return View(projectMember);
        }

        // GET: ProjectMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectMember projectMember = db.ProjectMembers.Find(id);
            if (projectMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "FullName", projectMember.PersonnelID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "Project_Title", projectMember.ProjectID);
            return View(projectMember);
        }

        // POST: ProjectMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectMembersID,ProjectID,PersonnelID")] ProjectMember projectMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "FullName", projectMember.PersonnelID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "Project_Title", projectMember.ProjectID);
            return View(projectMember);
        }

        // GET: ProjectMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectMember projectMember = db.ProjectMembers.Find(id);
            if (projectMember == null)
            {
                return HttpNotFound();
            }
            return View(projectMember);
        }

        // POST: ProjectMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectMember projectMember = db.ProjectMembers.Find(id);
            db.ProjectMembers.Remove(projectMember);
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
    }


    

}
