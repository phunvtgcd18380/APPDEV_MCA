using AppDev_MCA.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppDev_MCA.Controllers
{
    public class TraineesController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public TraineesController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext())
            );
        }
        // GET: Trainees
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewProfile()
        {
            var CurrentTraineeId = User.Identity.GetUserId();
            var TraineeInDb = _context.TraineeUsers.SingleOrDefault(t => t.Id == CurrentTraineeId);
            return View(TraineeInDb);
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string password)
        {
            var CurrentTraineeId = User.Identity.GetUserId();
            var TraineeInDb = _context.Users.SingleOrDefault(c => c.Id == CurrentTraineeId);
            string newPassword = password;
            _userManager.RemovePassword(CurrentTraineeId);
            _userManager.AddPassword(CurrentTraineeId, newPassword);
            _userManager.Update(TraineeInDb);
            return View();
        }
        public ActionResult ViewCourse(string searchString)
        {
            var course = _context.Courses.Include(c => c.Category).ToList();
            if (!searchString.IsNullOrWhiteSpace())
            {
                course = _context.Courses
                .Where(c => c.Name.Contains(searchString) || c.Category.Name.Contains(searchString))
                .Include(c => c.Category)
                .ToList();
            }
            return View(course);
        }
        public ActionResult ViewAssignedCourse()
        {
            var CurrentTraineeId = User.Identity.GetUserId();
            var traineeCourse = _context.TrainerCourses.Where(t => t.TrainerId == CurrentTraineeId).ToList();
            return View(traineeCourse);
        }
    }
}