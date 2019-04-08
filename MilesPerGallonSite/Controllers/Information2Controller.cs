using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MilesPerGallonSite.Models;

namespace MilesPerGallonSite.Controllers
{
    public class Information2Controller : Controller
    {
        private MilesPerGallonProjectEntities db = new MilesPerGallonProjectEntities();

        // GET: Information2
        public ActionResult Index()
        {
            var information = from m in db.Information.ToList() select m;
            List<InformationView> model = new List<InformationView>();

            
            foreach (var item in information)
            {
                InformationView driver = new InformationView();
                driver.MapDataToViewModel(item);
                model.Add(driver);
            }
            try
            {         
                ViewBag.AverageMPG = Math.Round(model.Average(s => s.MilesPerGallon), 2);
            }
            catch (Exception)
            {
                ViewBag.AverageMPG = 0;
            }
            return View(model.OrderByDescending(s => s.FillUpDate).ThenBy(s => s.LName).ThenBy(s => s.CarModel));

        }

        [HttpPost]
        public ActionResult Index(string searchName, string searchModel, DateTime? startDate, DateTime? endDate)
        {
            var information = from m in db.Information.ToList() select m;
            List<InformationView> model = new List<InformationView>();


            foreach (var item in information)
            {
                InformationView driver = new InformationView();
                driver.MapDataToViewModel(item);
                model.Add(driver);
            }

            var fueler = from f in model select f;
            if (startDate != null)
            {
                fueler = fueler.Where(s => s.FillUpDate >= startDate);
            }
            if (endDate != null)
            {
                fueler = fueler.Where(s => s.FillUpDate <= endDate);
            }
            if (!string.IsNullOrEmpty(searchModel))
            {
                fueler = fueler.Where(s => s.CarModel.ToLower().Contains(searchModel.ToLower()));
            }
            if (!string.IsNullOrEmpty(searchName))
            {
                fueler = fueler.Where(s => s.LName.ToLower().Contains(searchName.ToLower()) || s.FName.ToLower().Contains(searchName.ToLower()));
            }
            try
            {
                ViewBag.AverageMPG = Math.Round(fueler.Average(s => s.MilesPerGallon), 2);
            }
            catch (Exception)
            {
                ViewBag.AverageMPG = 0;
            }
            return View(fueler.OrderByDescending(s => s.FillUpDate).ThenBy(s => s.LName).ThenBy(s => s.CarModel));

        }


        // GET: Information2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Information information = db.Information.Find(id);
            if (information == null)
            {
                return HttpNotFound();
            }
            return View(information);
        }

        // GET: Information2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Information2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonId,FName,LName,CarModel,MilesDriven,GallonsFilled,FillUpDate")] Information information)
        {
            if (ModelState.IsValid)
            {
                db.Information.Add(information);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(information);
        }

        // GET: Information2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Information information = db.Information.Find(id);
            if (information == null)
            {
                return HttpNotFound();
            }
            return View(information);
        }

        // POST: Information2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonId,FName,LName,CarModel,MilesDriven,GallonsFilled,FillUpDate")] Information information)
        {
            if (ModelState.IsValid)
            {
                db.Entry(information).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(information);
        }

        // GET: Information2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Information information = db.Information.Find(id);
            if (information == null)
            {
                return HttpNotFound();
            }
            return View(information);
        }

        // POST: Information2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Information information = db.Information.Find(id);
            db.Information.Remove(information);
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
