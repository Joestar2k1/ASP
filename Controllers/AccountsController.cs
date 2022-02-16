using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FshopASP.Data;
using FshopASP.Models;
using Microsoft.AspNetCore.Http;

namespace FshopASP.Controllers
{
    public class AccountsController : Controller
    {
        private readonly FshopASPContext _context;

        public AccountsController(FshopASPContext context)
        {
            _context = context;
        }
        public  IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(String username, String password)
        {
            Account account = _context.Accounts.Where(acc => acc.Username == username)
                .Where(acc => acc.Password == password).FirstOrDefault();
            if (account != null)
            {
                
                HttpContext.Session.SetInt32("AccountID", account.Id);
                HttpContext.Session.SetString("FullName", account.FullName);
                HttpContext.Session.SetString("Username", account.Username);
                return  RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.errorMessage = "Error.......";
                return View("Login");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return View("Login");
        }
        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Accounts.ToListAsync());
        }

        public async Task<IActionResult> Profile()
        {
            ViewBag.Username = HttpContext.Session.GetString("FullName");
            string dark = HttpContext.Session.GetString("Username");
            var userItem = _context.Accounts.Where(acc=>acc.Username == dark);
            ViewBag.info = -1;
            return View(await userItem.ToListAsync());
        }
        //// GET: Accounts/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var account = await _context.Accounts
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(account);
        //}

        //// GET: Accounts/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Accounts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Username,Password,FullName,Address,Email,Image,Phone,Type,Status")] Account account)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(account);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(account);
        //}

        //// GET: Accounts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var account = await _context.Accounts.FindAsync(id);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(account);
        //}

        //// POST: Accounts/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,FullName,Address,Email,Image,Phone,Type,Status")] Account account)
        //{
        //    if (id != account.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(account);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AccountExists(account.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(account);
        //}

        //// GET: Accounts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var account = await _context.Accounts
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(account);
        //}

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
