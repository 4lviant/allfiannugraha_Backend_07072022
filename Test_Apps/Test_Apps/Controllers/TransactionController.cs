using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Apps.Models;

namespace Test_Apps.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TestP79Context _context;

        public TransactionController(TestP79Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Transaction = _context.Transaksis.ToList();
            return View(Transaction);
        }

        public ActionResult Create()
        {
            ViewBag.CustommerList = _context.Nasabahs.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaksi Model)
        {
            Transaksi item = new Transaksi();
            item.AccountId = Model.AccountId;
            item.TransactionDate = Model.TransactionDate;
            item.Description = Model.Description;
            item.DebitCreditStatus = Model.DebitCreditStatus;
            item.Amount = Model.Amount;
            _context.Add(item);
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int TransactionId)
        {
            var Item = _context.Transaksis.Find(TransactionId);
            return View(Item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaksi Model)
        {
            Transaksi item = _context.Transaksis.Find(Model.TransactionId);
            item.TransactionId = Model.TransactionId;
            item.AccountId = Model.AccountId;
            item.TransactionDate = Model.TransactionDate;
            item.Description = Model.Description;
            item.DebitCreditStatus = Model.DebitCreditStatus;
            item.Amount = Model.Amount;
            _context.Update(item);
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }     

       
        public ActionResult Delete(int TransactionId)
        {
            var item =  _context.Transaksis.Find(TransactionId);
            _context.Transaksis.Remove(item);
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Point()
        {
            
            List<Point> point = new List<Point>();
            var trans = _context.Transaksis.ToList();
            foreach (var item in trans)
            {
                Point pointItem = new Point();
                pointItem.AccountId = item.AccountId;                
                pointItem.Name = _context.Nasabahs.Where(x => x.AccountId == item.AccountId).Select(y => y.Name).FirstOrDefault();
                pointItem.TPoint = getPoint(item.Description,Convert.ToDouble(item.Amount));
                pointItem.Amount = item.Amount;
                point.Add(pointItem);
            }
            return View(point);
        }

        private int getPoint(string Trans, double Amount)
        {
            int Point = 0;
            if (Trans.ToLower() == "beli pulsa")
            {
                if (Amount >= 0 && Amount <= 10000)
                {
                    Point = 0;
                }
                if (Amount > 30000)
                {
                    Point = Point + Convert.ToInt32((Amount - 30000) / 1000 * 2);
                    Point = Point + Convert.ToInt32((30000 - 10000) / 1000 * 1);
                }
                else if(Amount > 10001 && Amount <=30000) {
                    Point = Point + Convert.ToInt32((Amount - 10000) / 1000 * 1);
                }
            }
            else if (Trans.ToLower() == "bayar listrik")
            {
                if (Amount >= 0 && Amount <= 50000)
                {
                    Point = 0;
                }
                if (Amount > 100000)
                {
                    Point = Point + Convert.ToInt32((Amount - 100000) / 2000 * 2);
                    Point = Point + Convert.ToInt32((100000 - 50000) / 2000 * 1);
                }
                else if (Amount > 10001 && Amount <= 30000)
                {
                    Point = Point + Convert.ToInt32((Amount - 10000) / 1000 * 1);
                }
            }

            return Point;
        }

        public IActionResult Report(int? AccountId, DateTime? Start, DateTime? End)
        {
            List<Report> reports = new List<Report>();
            var trans = _context.Transaksis.ToList();
            if (AccountId != null && Start != null && End != null)
            {
                trans = trans.Where(x => x.AccountId == AccountId && x.TransactionDate >= Start && x.TransactionDate <= End).ToList();
            }
            foreach (var item in trans)
            {
                Report reportsItem = new Report();
                reportsItem.TransactionDate = item.TransactionDate;
                reportsItem.Description = item.Description;
                reportsItem.Debit = item.DebitCreditStatus == "D" ? item.Amount.ToString() : "-";
                reportsItem.Credit = item.DebitCreditStatus == "C" ? item.Amount.ToString() : "-";
                reportsItem.Amount = item.Amount;
                reports.Add(reportsItem);
            }
            return View(reports);
        }
    }
}
