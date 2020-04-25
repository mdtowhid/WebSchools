using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models
{
    [Table("PriorExperiences")]
    public class PriorExperience
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Project Idea")]
        public string ProjectIdea { get; set; }
        [Required]
        [Display(Name = "Project Category")]
        public string ProjectCategory { get; set; }
        [Required]
        public string Problem { get; set; }
    }
}