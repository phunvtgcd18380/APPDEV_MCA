using AppDev_MCA.Models;
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
        public ActionResult RemoveTrainingStaffAccount(string id)
        {
            var userInDb = _context.Users.SingleOrDefault(s => s.Id == id);
            _context.Users.Remove(userInDb);
            _context.SaveChanges();
            return View("Index");
        }
        [HttpGet]
        public ActionResult CreateTrainingStaff()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateTrainingStaff(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user.Id, "STAFF");
            }
            _context.SaveChanges();
            return View(model);
        }


        public ActionResult ResetPassword(string id)
        {
            var user = _userManager.FindById(id);
            _userManager.RemovePassword(user.Id);
            _userManager.AddPassword(user.Id, "123456");
            _userManager.Update(user);
            return View();
        }
        public ActionResult RemoveTranerAccount(string id)
        {
            var userInDb = _context.Users.SingleOrDefault(s => s.Id == id);
            var trainerInDb = _context.TrainerUsers.SingleOrDefault(t => t.Id == id);
            _context.TrainerUsers.Remove(trainerInDb);
            _context.Users.Remove(userInDb);
            _context.SaveChanges();
            return View("Index");
        }
        public ActionResult CreateTrainerAccount()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateTrainerAccount(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user.Id, "TRAINER");
                var trainerUser = new TrainerUser()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FullName = model.FullName,
                    Telephone = model.Telephone,
                    WorkingPlace = model.WorkingPlace,
                    type = model.Type
                };
                _context.TrainerUsers.Add(trainerUser);
            }
            _context.SaveChanges();
            return View(model);
        }
    }
}