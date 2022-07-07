using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Apps.Models;

namespace Test_Apps.Controllers
{
    public class NasabahController : Controller
    {
        private readonly TestP79Context _context;

        public NasabahController(TestP79Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var nasabah = _context.Nasabahs.ToList();
            return View(nasabah);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Nasabah Model)
        {
            Nasabah item = new Nasabah();
            item.Name = Model.Name;
            _context.Add(item);
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int AccountId)
        {
            var Item = _context.Nasabahs.Find(AccountId);
            return View(Item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Nasabah Model)
        {
            Nasabah Item = _context.Nasabahs.Find(Model.AccountId);
            Item.AccountId = Model.AccountId;
            Item.Name = Model.Name;
            _context.Update(Item);
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }     

       
        public ActionResult Delete(int AccountId)
        {
            var item =  _context.Nasabahs.Find(AccountId);
            _context.Nasabahs.Remove(item);
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
