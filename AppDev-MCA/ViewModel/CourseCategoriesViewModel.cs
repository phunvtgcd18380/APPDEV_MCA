using AppDev_MCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppDev_MCA.ViewModel
{
    public class CourseCategoriesViewModel
    {
        public Course Courses { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}