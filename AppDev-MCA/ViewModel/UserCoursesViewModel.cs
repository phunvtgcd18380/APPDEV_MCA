using AppDev_MCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppDev_MCA.ViewModel
{
    public class UserCoursesViewModel
    {
        public ApplicationUser User { get; set; }
        public TrainerCourse TrainerUser { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}