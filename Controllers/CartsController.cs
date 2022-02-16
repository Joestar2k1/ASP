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
    public class CartsController : Controller
    {
        private readonly FshopASPContext _context;

        public CartsController(FshopASPContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
           string username = HttpContext.Session.GetString("Username");
            var listCart = _context.Carts.Include(c => c.Account).Include(c=>c.Product).Where(c=>c.Account.Username== username);
         
            return View(await listCart.ToListAsync());
        }
        public async Task<IActionResult> Checkout()
        {   string username = HttpContext.Session.GetString("Username");
            if (!CheckStock())
            {
                ViewBag.Username = username;
                var model = _context.Carts.Include(c => c.Account).Include(c => c.Product).Where(c => c.Account.Username == username);
                return View("Index",model);
            }
            ViewBag.Username =username;
            var getInfoUser = _context.Carts.Include(c => c.Account).Include(c => c.Product).Where(c=>c.Account.Username==username);
            return View(await getInfoUser.ToListAsync());
        }


        public IActionResult AddToCart(int id)
        {
            return AddToCart(id, 1);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId,int quantity)
        {
            string username = HttpContext.Session.GetString("Username");
            int accountId = _context.Accounts.FirstOrDefault(acc => acc.Username == username).Id;
            Cart carts = _context.Carts.FirstOrDefault(c => c.AccountId == accountId && c.ProductId == productId);
            if (carts == null)
            {
                carts = new Cart();
                carts.ProductId = productId;
                carts.AccountId = accountId;
                carts.Quantity = quantity;
                _context.Carts.Add(carts);
            
            }
            else
            {
                carts.Quantity += quantity;
             
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> UpdateCart()
        {
            string username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            var getInfoUser = _context.Carts.Include(c => c.Account).Include(c => c.Product).Where(c => c.Account.Username == username);
            return View(await getInfoUser.ToListAsync());
        }

        public async Task<IActionResult> PayCart()
        {
            Random random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);

         
            string username = HttpContext.Session.GetString("Username");
            int accountId = _context.Carts.FirstOrDefault(ca => ca.Account.Username == username).AccountId;
            List<Cart> cart = _context.Carts.Where(c => c.AccountId == accountId).ToList();
            int total = _context.Carts.Where(ca => ca.Account.Username == username).Sum(c => c.Quantity * c.Product.Price);

            //tạo hóa đơn
            Invoice invoices = new Invoice();
            DateTime now =DateTime.Now;
            invoices.AccountId = accountId;
            invoices.Code = finalString;
            invoices.IsPaid = false;
            invoices.OrderDate = now;
            invoices.ShippingAddress = _context.Accounts.FirstOrDefault(ca => ca.Username == username).Username;
            invoices.ShippingPhone = _context.Accounts.FirstOrDefault(ca => ca.Username == username).Phone;
            invoices.ShippingName = _context.Accounts.FirstOrDefault(ca => ca.Username == username).Address;
            invoices.Total = total;
            invoices.Status = true;
            _context.Invoices.Add(invoices);
            _context.SaveChanges();

      
            //tạo chi tiết hóa đơn
         

            foreach(var item in cart)
            {
                InvoiceDetail invoiceDetails = new InvoiceDetail();
                invoiceDetails.InvoiceId = invoices.Id;
                invoiceDetails.ProductId =item.ProductId;
                invoiceDetails.Quantity = item.Quantity;
                invoiceDetails.Total = _context.Products.FirstOrDefault(pr=>pr.Id == item.ProductId).Price *item.Quantity;
                _context.InvoiceDetails.Add(invoiceDetails);
            }    
            _context.SaveChanges();

            //xóa giỏ hàng
        
            foreach (var item in cart)
            {
                Cart carts = _context.Carts.Find(item.Id);
                _context.Carts.Remove(carts);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Carts");
        }
        public bool CheckStock()
        {
            List<Cart> cart = _context.Carts.Include(c=>c.Product).Include(c => c.Account).ToList();
            foreach(var item in cart)
            {
                if(item.Quantity > item.Product.Quantity)
                {
                    return false;
                }
            }
            return true;
        }
        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
