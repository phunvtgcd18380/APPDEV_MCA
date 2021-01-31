using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppDev_MCA.Models
{
    public class TraineeUser
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string mainProgrammingLanguage { get; set; }
        public string ToeicScore { get; set; }
        public string Department { get; set; }
        public string EmailAddress { get; set; }
        [ForeignKey("Id")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}