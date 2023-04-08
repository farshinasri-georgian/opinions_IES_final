using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using opinions.Data;
using opinions.Models;
using System.Diagnostics;

namespace opinions.Controllers
{
    public class HomeController : Controller
    {


        private ApplicationDbContext _application;
        public HomeController(ApplicationDbContext application)
        {
            _application = application;
        }

        
        public  ActionResult DeleteUser(String email)
        {


            var user = _application.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower()); ;

            if (user == null) 
            { 
                return View("Index");
            }
            _application.Users.Remove(user);
            _application.SaveChanges();
            return View("deleted");
            
            
        }
        public ActionResult deleted()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> UserList()
        {
            var users = await _application.Users.ToListAsync();
            ViewBag.Users = users;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}