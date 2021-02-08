﻿using AppDev_MCA.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppDev_MCA.Controllers
{
    public class TrainersController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public TrainersController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext())
            );
        }
        // GET: Trainers
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UpdateProfile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateProfile(TrainerUser trainer)
        {
            var CurrentUserId = User.Identity.GetUserId();
            var UserInDb = _context.Users.SingleOrDefault(c => c.Id == CurrentUserId);
            var TrainerInDb = _context.TrainerUsers.SingleOrDefault(c => c.Id == CurrentUserId);
            UserInDb.Email = trainer.EmailAddress;
            UserInDb.UserName = trainer.EmailAddress;
            TrainerInDb.FullName = trainer.FullName;
            TrainerInDb.EmailAddress = trainer.EmailAddress;
            TrainerInDb.Telephone = trainer.Telephone;
            TrainerInDb.type = trainer.type;
            TrainerInDb.WorkingPlace = trainer.WorkingPlace;
            TrainerInDb.UserName = trainer.EmailAddress;
            _context.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult ChangePassword ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string password)
        {
            var CurrentTrainerId = User.Identity.GetUserId();
            var TrainerInDb = _context.Users.SingleOrDefault(c => c.Id == CurrentTrainerId);
            string newPassword = password;
            _userManager.RemovePassword(CurrentTrainerId);
            _userManager.AddPassword(CurrentTrainerId, newPassword);
            _userManager.Update(TrainerInDb);
            return View();
        }
        public ActionResult ViewAssignedCourse()
        {
            var CurrentTrainerId = User.Identity.GetUserId();
            var trainerCourse = _context.TrainerCourses.Where(t => t.TrainerId == CurrentTrainerId).ToList();
            return View(trainerCourse);
        }
    }
}