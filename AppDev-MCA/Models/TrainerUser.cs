using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppDev_MCA.Models
{
    public class TrainerUser
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string Telephone { get; set; }
        public string WorkingPlace { get; set; }
        public string EmailAddress { get; set; }
        [ForeignKey("Id")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}