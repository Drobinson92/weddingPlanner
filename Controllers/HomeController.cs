using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using weddingplanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace weddingplanner.Controllers
{
    public class HomeController : Controller
    {
        private PlannerContext _context;
        public HomeController(PlannerContext context){
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel user){
            Console.WriteLine("registering...");
            if(ModelState.IsValid){
                User check = _context.Users.SingleOrDefault(a => a.Email == user.Email);
                if(check != null){
                    ViewBag.Error = "User already exists";
                    return View("Index");
                }
                PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                user.Password = Hasher.HashPassword(user, user.Password);
                User u = new User{
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                // HttpContext.Session.SetString("email", u.Email);
                System.Console.WriteLine("about to save");
                _context.Add(u);
                _context.SaveChanges();
                User newUser = _context.Users.SingleOrDefault(a => a.Email == user.Email);
                HttpContext.Session.SetInt32("id", newUser.UserId);
                System.Console.WriteLine("about to redirect");
                return RedirectToAction("Wedding", "Wedding");
            }
            return View("Index");
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel user){
            System.Console.WriteLine("logging in");
            // List<User> u = _context.Users.Where(a => a.Email == user.Email).ToList();
            if(ModelState.IsValid){

            User u = _context.Users.SingleOrDefault(a => a.Email == user.Email);
            System.Console.WriteLine("got a user");
            if(u != null){
            if(user.Email != null && user.Password != null){
                var Hasher = new PasswordHasher<LoginViewModel>();
                if(0 != Hasher.VerifyHashedPassword(user, u.Password, user.Password)){
                    HttpContext.Session.SetInt32("id", u.UserId);
                    return RedirectToAction("Wedding", "Wedding");
                }
            }
            }
            }
            return View("Index");
        }
               public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        }
}

