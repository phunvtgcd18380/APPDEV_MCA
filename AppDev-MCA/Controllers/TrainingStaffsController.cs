using AppDev_MCA.Models;
using AppDev_MCA.ViewModel;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppDev_MCA.Controllers
{
    [Authorize(Roles = "STAFF")]
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
            return View();
        }
        public ActionResult ManageAccount()
        {
            return View();
        }
        public ActionResult ListCategory(string searchString)
        {
            var categories = _context.Categories.ToList();
            if (!searchString.IsNullOrWhiteSpace())
            {
                categories = _context.Categories
                .Where(c => c.Name.Contains(searchString))
                .ToList();
            }
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
            return RedirectToAction("ListCategory");
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var newCategories = new Category()
                {
                    Name = category.Name,
                    Description = category.Description
                };
                _context.Categories.Add(newCategories);
                _context.SaveChanges();
                return RedirectToAction("ListCategory");
            }
            return View(category);
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
            return RedirectToAction("ListCategory");
        }


        public ActionResult ListCourse(string searchString)
        {
            var course = _context.Courses.Include(c => c.Category).ToList();
            if (!searchString.IsNullOrWhiteSpace())
            {
                course = _context.Courses
                .Where(c => c.Name.Contains(searchString) || c.Category.Name.Contains(searchString) )
                .Include(c => c.Category)
                .ToList();
            }
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
            return RedirectToAction("ListCourse");
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
            if (ModelState.IsValid)
            {
                var newCourse = new Course()
                {
                    Name = course.Courses.Name,
                    Description = course.Courses.Description,
                    CategoryId = course.Courses.CategoryId
                };
                _context.Courses.Add(newCourse);
                _context.SaveChanges();
                return RedirectToAction("ListCourse");
            }
            return View(course);
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
            return RedirectToAction("ListCourse");
        }


        
        public ActionResult ListTrainer(string searchString)
        {
            var trainerInDb = _context.TrainerUsers.ToList();
            if (!searchString.IsNullOrWhiteSpace())
            {
                trainerInDb = _context.TrainerUsers
                .Where(m => m.FullName.Contains(searchString) || m.Telephone.Contains(searchString))
                .ToList();
            }
            return View(trainerInDb);
        }
        [HttpGet]
        public ActionResult UpdateProfileTrainer(string id)
        {
            var trainerInDb = _context.TrainerUsers.SingleOrDefault(t => t.Id == id);
            return View(trainerInDb);
        }
        [HttpPost]
        public ActionResult UpdateProfileTrainer(TrainerUser trainer)
        {
            var trainerInDb = _context.TrainerUsers.SingleOrDefault(t => t.Id == trainer.Id);
            {
                trainerInDb.UserName = trainer.EmailAddress;
                trainerInDb.FullName = trainer.FullName;
                trainerInDb.WorkingPlace = trainer.WorkingPlace;
                trainerInDb.type = trainer.type;
                trainerInDb.Telephone = trainer.Telephone;
                trainerInDb.EmailAddress = trainer.EmailAddress;
            }
            var userInDb = _context.Users.SingleOrDefault(u => u.Id == trainer.Id);
            {
                userInDb.UserName = trainer.EmailAddress;
            }
            _context.SaveChanges();
            return RedirectToAction("ListTrainer");
        }
        public ActionResult DetailProfileTrainer(string id)
        {
            var trainerInDb = _context.TrainerUsers.SingleOrDefault(t => t.Id == id);
            return View(trainerInDb);
        }
        public ActionResult RemoveTrainer(string id)
        {
            var UserInDb = _context.Users.SingleOrDefault(t => t.Id == id);
            var TrainerInDb = _context.TrainerUsers.SingleOrDefault(t => t.Id == id);
            _context.TrainerUsers.Remove(TrainerInDb);
            _context.Users.Remove(UserInDb);
            _context.SaveChanges();
            return RedirectToAction("ListTrainer");
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
            return RedirectToAction("ListTrainer");
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
            return RedirectToAction("ListTrainer");
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
            return RedirectToAction("ListTrainer");
        }

        public ActionResult ListTrainee(string searchString)
        {
            var traineeInDb = _context.TraineeUsers.ToList();
            if (!searchString.IsNullOrWhiteSpace())
            {
                traineeInDb = _context.TraineeUsers
                .Where(m => m.FullName.Contains(searchString) || m.Telephone.Contains(searchString))
                .ToList();
            }
            return View(traineeInDb);
        }
        public ActionResult DetailTraineeProfile(string id)
        {
            var traineeInDb = _context.TraineeUsers.SingleOrDefault(t => t.Id == id);
            return View(traineeInDb);
        }
        public ActionResult CreateTraineeAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTraineeAccount(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var today = DateTime.Today;
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = _userManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    _userManager.AddToRole(user.Id, "TRAINEE");
                    var traineeUser = new TraineeUser()
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        FullName = model.FullName,
                        DateOfBirth = model.DateOfBirth,
                        age = today.Year - model.DateOfBirth.Year,
                        Telephone = model.Telephone,
                        mainProgrammingLanguage = model.mainProgrammingLangueage,
                        ToeicScore = model.ToeicSocre,
                        Department = model.Department,
                        EmailAddress = user.UserName
                    };
                    _context.TraineeUsers.Add(traineeUser);
                    _context.SaveChanges();
                    return RedirectToAction("ListTrainee");
                }
            }
            return View(model);
        }

        public ActionResult ResetPassword(string id)
        {
            var user = _userManager.FindById(id);
            _userManager.RemovePassword(user.Id);
            _userManager.AddPassword(user.Id, "12345678");
            _userManager.Update(user);
            return RedirectToAction("ManageAccount");
        }
        public ActionResult RemoveTrainee(string id)
        {
            var UserInDb = _context.Users.SingleOrDefault(t => t.Id == id);
            var TraineeInDb = _context.TraineeUsers.SingleOrDefault(t => t.Id == id);
            _context.TraineeUsers.Remove(TraineeInDb);
            _context.Users.Remove(UserInDb);
            _context.SaveChanges();
            return RedirectToAction("ListTrainee"); ;
        }
        public ActionResult RemoveCourseTrainee(int id)
        {
            var traineeCourseInDb = _context.TraineeCourses.SingleOrDefault(c => c.Id == id);
            _context.TraineeCourses.Remove(traineeCourseInDb);
            _context.SaveChanges();
            return RedirectToAction("ViewCourseAssignedTrainee");
        }
        [HttpGet]
        public ActionResult AssignCourseTrainee(string id)
        {
            var UserInDb = _context.Users.SingleOrDefault(t => t.Id == id);
            var viewModel = new TraineeUserCoursesViewModel()
            {
                User = UserInDb,
                Courses = _context.Courses.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AssignCourseTrainee(TraineeUserCoursesViewModel traineeCourse)
        {
            var newTraineeCourse = new TraineeCourse()
            {
                TraineeId = traineeCourse.User.Id,
                CourseId = traineeCourse.TraineeUser.CourseId,
            };
            var TraineeCourseInDb = _context.TraineeCourses.Add(newTraineeCourse);
            var traineeUserObject = _context.TraineeUsers.SingleOrDefault(t => t.Id == TraineeCourseInDb.TraineeId);
            var CourseObject = _context.Courses.SingleOrDefault(t => t.Id == TraineeCourseInDb.CourseId);
            TraineeCourseInDb.TraineeName = traineeUserObject.UserName;
            TraineeCourseInDb.CourseName = CourseObject.Name;
            _context.SaveChanges();
            return RedirectToAction("ListTrainee");
        }
        [HttpGet]
        public ActionResult ChangeCourseTrainee(int id)
        {
            var traineeCourseInDb = _context.TraineeCourses.SingleOrDefault(t => t.Id == id);
            var viewModel = new TraineeUserCoursesViewModel()
            {
                TraineeUser = traineeCourseInDb,
                Courses = _context.Courses.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult ChangeCourseTrainee(TraineeUserCoursesViewModel traineeCourse)
        {
            var traineeCourseInDb = _context.TraineeCourses.SingleOrDefault(t => t.Id == traineeCourse.TraineeUser.Id);
            var courseInDb = _context.Courses.SingleOrDefault(t => t.Id == traineeCourse.TraineeUser.CourseId);
            traineeCourseInDb.CourseId = traineeCourse.TraineeUser.CourseId;
            traineeCourseInDb.CourseName = courseInDb.Name;
            _context.SaveChanges();
            return RedirectToAction("ListTrainee");
        }
        public ActionResult ViewCourseAssignedTrainee(string id)
        {
            var traineeCourse = _context.TraineeCourses.Where(t => t.TraineeId == id).ToList();
            return View(traineeCourse);
        }
    }
}