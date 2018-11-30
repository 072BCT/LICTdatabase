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
    public class PersonnelsController : Controller
    {
        private Entities db = new Entities();

       
        public ActionResult Index(string option, string search)
        {

            //if a user choose the radio button option as Subject  
            if (option == "FistName")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.Personnels.Where(x => x.FirstName == search || search == null).ToList());
            }
            else if (option == "Title")
            {
                return View(db.Personnels.Where(x => x.Urls == search || search == null).ToList());
            }
            else
            {
                return View(db.Personnels.Where(x => x.LastName.StartsWith(search) || search == null).ToList());
            }
            //var personnels = db.Personnels.Include(p => p.LICT_Title1);
            //return View(personnels.ToList());
        }

        // GET: Personnels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel = db.Personnels.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // GET: Personnels/Create
        public ActionResult Create()
        {
            ViewBag.LICT_Title = new SelectList(db.LICT_Title, "Id", "Title");
            return View();
        }

        // POST: Personnels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                personnel.Image = new byte[personnel.UploadImages.ContentLength];
                personnel.UploadImages.InputStream.Read(personnel.Image, 0, personnel.Image.Length);

                db.Personnels.Add(personnel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LICT_Title = new SelectList(db.LICT_Title, "Id", "Title", personnel.LICT_Title);
            return View(personnel);
        }

        // GET: Personnels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel = db.Personnels.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            ViewBag.LICT_Title = new SelectList(db.LICT_Title, "Id", "Title", personnel.LICT_Title);
            return View(personnel);
        }

        // POST: Personnels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonnelID,FullName,Title,FirstName,LastName,LICT_Title,PostAtIOE,Email,PhoneNo,Urls,Image")] Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personnel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LICT_Title = new SelectList(db.LICT_Title, "Id", "Title", personnel.LICT_Title);
            return View(personnel);
        }

        // GET: Personnels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel = db.Personnels.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // POST: Personnels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personnel personnel = db.Personnels.Find(id);
            db.Personnels.Remove(personnel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UserView()
        {
            var personnels = db.Personnels.Include(p => p.LICT_Title1);
          
            return View(personnels.ToList());
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
