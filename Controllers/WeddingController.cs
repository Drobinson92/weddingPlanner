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

namespace weddingplanner.Controllers{
    public class WeddingController : Controller{
            private PlannerContext _context;
    public WeddingController(PlannerContext context){
        _context = context;
    }
    public IActionResult Wedding(){
        if(HttpContext.Session.GetInt32("id")==null){
            return RedirectToAction("Index", "Home");
        }
        ViewBag.loggedId = HttpContext.Session.GetInt32("id");
        ViewBag.Weddings = _context.Weddings.Include(a => a.UsersWeddings).ThenInclude(a => a.Users).ToList();
        return View();
    }
    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return RedirectToAction("Index","Home");
    }
    public IActionResult WeddingForm(){
        return View();
    }
    public IActionResult addWedding(Wedding formWed){
        if(ModelState.IsValid){
            User u = _context.Users.Include(a => a.UsersWeddings).ThenInclude(a => a.Weddings).SingleOrDefault(a => a.UserId == HttpContext.Session.GetInt32("id"));
            Wedding newWed = new Wedding{
                WedderOne = formWed.WedderOne,
                WedderTwo = formWed.WedderTwo,
                WeddingDate = formWed.WeddingDate,
                WeddingAddress = formWed.WeddingAddress,
                CreatedBy = u.UserId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _context.Weddings.Add(newWed);
            _context.SaveChanges();
            return RedirectToAction("Wedding");
        }
        return View("WeddingForm");   
    }
    public IActionResult WeddingInfo(int id){
        Wedding w = _context.Weddings.Include(a => a.UsersWeddings).ThenInclude(a => a.Users).SingleOrDefault(u => u.WeddingId == id);
        ViewBag.wedding = w;
        return View();
    }
    public IActionResult Delete(int id){
        Wedding w = _context.Weddings.SingleOrDefault(wed => wed.WeddingId == id);
        _context.Weddings.Remove(w);
        _context.SaveChanges(); 
        return RedirectToAction("Wedding");
    }
    public IActionResult RSVP(int id){
        Wedding w = _context.Weddings.Include(a => a.UsersWeddings).ThenInclude(a => a.Users).SingleOrDefault(a => a.WeddingId == id);
        UsersWeddings uw = new UsersWeddings{UsersId = (int) HttpContext.Session.GetInt32("id"), WeddingsId = w.WeddingId};
        _context.UsersWeddings.Add(uw);
        _context.SaveChanges();
        return RedirectToAction("Wedding");
    }
    public IActionResult unRSVP(int id){
        UsersWeddings uw = _context.UsersWeddings.SingleOrDefault(a => a.UsersId == (int) HttpContext.Session.GetInt32("id") && a.WeddingsId == id);
        _context.UsersWeddings.Remove(uw);
        _context.SaveChanges();
        return RedirectToAction("Wedding");
    }
    }
}