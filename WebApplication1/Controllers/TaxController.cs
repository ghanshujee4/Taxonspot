using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1;

namespace WebApplication1.Controllers
{
    public class TaxController : Controller
    {
        private TaxEntities db = new TaxEntities();

        // GET: /Tax/
        public ActionResult Index()
        {
            return View(db.Tax_Table.ToList());
        }

        // GET: /Tax/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tax_Table tax_table = db.Tax_Table.Find(id);
            if (tax_table == null)
            {
                return HttpNotFound();
            }
            return View(tax_table);
        }

        // GET: /Tax/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Tax/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PAN,DOB,Mobile,Address,EmailID,FathersName,City,VAT")] Tax_Table tax_table)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    db.Tax_Table.Add(tax_table);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(tax_table);
            }

            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Tax_Table", "Create"));
            }
        }
        // GET: /Tax/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tax_Table tax_table = db.Tax_Table.Find(id);
            if (tax_table == null)
            {
                return HttpNotFound();
            }
            return View(tax_table);
        }

        // POST: /Tax/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,PAN,DOB,Mobile,Address,EmailID,FathersName,City,VAT")] Tax_Table tax_table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tax_table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            
            return View(tax_table);
        }

        // GET: /Tax/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tax_Table tax_table = db.Tax_Table.Find(id);
            if (tax_table == null)
            {
                return HttpNotFound();
            }
            return View(tax_table);
        }

        // POST: /Tax/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tax_Table tax_table = db.Tax_Table.Find(id);
            db.Tax_Table.Remove(tax_table);
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
