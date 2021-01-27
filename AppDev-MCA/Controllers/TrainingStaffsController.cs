using AppDev_MCA.Models;
using AppDev_MCA.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppDev_MCA.Controllers
{
    public class TrainingStaffsController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public TrainingStaffsController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext())
            );
        }
        // GET: TrainingStaffs
        public ActionResult Index()
        {
            var trainerUsers = _context.TrainerUsers.ToList();
            return View(trainerUsers);
        }
        public ActionResult ListCategory()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        public ActionResult DetailCategory(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);

            return View(categoryInDb);
        }
        public ActionResult DeleteCategory(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);
            _context.Categories.Remove(categoryInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            var newCategories = new Category()
            {
                Name = category.Name,
                Description = category.Description
            };
            _context.Categories.Add(newCategories);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);
            return View(categoryInDb);
        }
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == category.Id);
            categoryInDb.Name = category.Name;
            categoryInDb.Description = category.Description;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ListCourse()
        {
            var course = _context.Courses.ToList();
            return View(course);
        }
        public ActionResult DetailCourse(int id)
        {
            var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == id);
            var viewModel = new CourseCategoriesViewModel()
            {
                Courses = courseInDb,
                Categories = _context.Categories.Where(c => c.Id == courseInDb.CategoryId).ToList()
            };
            return View(viewModel);
        }
        public ActionResult DeleteCourse(int id)
        {
            var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == id);
            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CreateCourse()
        {
            var viewModel = new CourseCategoriesViewModel()
            {
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult CreateCourse(CourseCategoriesViewModel course)
        {
            var newCourse = new Course()
            {
                Name = course.Courses.Name,
                Description = course.Courses.Description,
                CategoryId = course.Courses.CategoryId
            };
            _context.Courses.Add(newCourse);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == id);
            var viewModel = new CourseCategoriesViewModel()
            {
                Courses = courseInDb,
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult EditCourse(CourseCategoriesViewModel course)
        {
            var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == course.Courses.Id);
            courseInDb.Name = course.Courses.Name;
            courseInDb.Description = course.Courses.Description;
            courseInDb.CategoryId = course.Courses.CategoryId;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult ViewCourseAssigned(string id)
        {
            var trainerCourse = _context.TrainerCourses.Where(t => t.TrainerId == id).ToList();
            return View(trainerCourse);
        }
        public ActionResult RemoveCourse(int id)
        {
            var trainerCourseInDb = _context.TrainerCourses.SingleOrDefault(c => c.Id == id);
            _context.TrainerCourses.Remove(trainerCourseInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult AssignCourse(string id)
        {
            var UserInDb = _context.Users.SingleOrDefault(t => t.Id == id);
            var viewModel = new UserCoursesViewModel()
            {
                User = UserInDb,
                Courses = _context.Courses.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AssignCourse(UserCoursesViewModel trainerCourse)
        {

            var newTrainerCourse = new TrainerCourse()
            {
                TrainerId = trainerCourse.User.Id,
                CourseId = trainerCourse.TrainerUser.CourseId,
            };
            var TrainerCourseInDb = _context.TrainerCourses.Add(newTrainerCourse);
            var trainerUserObject = _context.TrainerUsers.SingleOrDefault(t => t.Id == TrainerCourseInDb.TrainerId);
            var CourseObject = _context.Courses.SingleOrDefault(t => t.Id == TrainerCourseInDb.CourseId);
            TrainerCourseInDb.TrainerName = trainerUserObject.UserName;
            TrainerCourseInDb.CourseName = CourseObject.Name;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChangeCourse(int id)
        {
            var trainerCourseInDb = _context.TrainerCourses.SingleOrDefault(t => t.Id == id);
            var viewModel = new UserCoursesViewModel()
            {
                TrainerUser = trainerCourseInDb,
                Courses = _context.Courses.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult ChangeCourse(UserCoursesViewModel trainerCourse)
        {
            var trainerCourseInDb = _context.TrainerCourses.SingleOrDefault(t => t.Id == trainerCourse.TrainerUser.Id);
            var courseInDb = _context.Courses.SingleOrDefault(t => t.Id == trainerCourse.TrainerUser.CourseId);
            trainerCourseInDb.CourseId = trainerCourse.TrainerUser.CourseId;
            trainerCourseInDb.CourseName = courseInDb.Name;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}