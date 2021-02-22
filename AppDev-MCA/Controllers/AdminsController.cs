using AppDev_MCA.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppDev_MCA.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminsController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public AdminsController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext())
            );
        }
        // GET: Admins
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListTrainingStaff()
        {
            var roleid = _context.Roles.Where(x => x.Name.Equals("STAFF")).FirstOrDefault().Id;
            var staffInDb = _context.Users.Where(s => s.Roles.Any(r => r.RoleId == roleid));
            return View(staffInDb);
        }
        public ActionResult RemoveTrainingStaffAccount(string id)
        {
            var userInDb = _context.Users.SingleOrDefault(s => s.Id == id);
            _context.Users.Remove(userInDb);
            _context.SaveChanges();
            return RedirectToAction("ListTrainingStaff");
        }
        [HttpGet]
        public ActionResult CreateTrainingStaff()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTrainingStaff(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = _userManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                 _userManager.AddToRole(user.Id, "STAFF");
            }
            _context.SaveChanges();
            if (result.Succeeded)
            {
                return RedirectToAction("ListTrainingStaff");
            }
            return View(model);
        }


        public ActionResult ListTrainer(string searchString)
        {
            var TrainerInDb = _context.TrainerUsers.ToList();
            if (!searchString.IsNullOrWhiteSpace())
            {
                TrainerInDb = _context.TrainerUsers
                .Where(m => m.FullName.Contains(searchString))
                .ToList();
            }
            return View(TrainerInDb);
        }
        public ActionResult ResetPassword(string id)
        {
            var user = _userManager.FindById(id);
             _userManager.RemovePassword(user.Id);
             _userManager.AddPassword(user.Id, "12345678");
             _userManager.Update(user);
            return RedirectToAction("ListTrainingStaff");
        }
        public ActionResult RemoveTrainerAccount(string id)
        {
            var userInDb = _context.Users.SingleOrDefault(s => s.Id == id);
            var trainerInDb = _context.TrainerUsers.SingleOrDefault(t => t.Id == id);
            _context.TrainerUsers.Remove(trainerInDb);
            _context.Users.Remove(userInDb);
            _context.SaveChanges();
            return RedirectToAction("ListTrainer");
        }
        public ActionResult CreateTrainerAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTrainerAccount(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result =  _userManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                 _userManager.AddToRole(user.Id, "TRAINER");
                var trainerUser = new TrainerUser()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FullName = model.FullName,
                    Telephone = model.Telephone,
                    WorkingPlace = model.WorkingPlace,
                    type = model.Type,
                    EmailAddress = user.UserName
                };
                _context.TrainerUsers.Add(trainerUser);
            }
            _context.SaveChanges();
            if (result.Succeeded)
            {
                return RedirectToAction("ListTrainer");
            }
            return View(model);
        }
    }
}