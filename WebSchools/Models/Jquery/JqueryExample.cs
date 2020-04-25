using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models.Jquery
{
    [Table("JqueryExamples")]
    public class JqueryExample
    {
        public int Id { get; set; }
        [Required]
        public string ExampleHeading { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Example { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public int Sort { get; set; }

        public int JqueryIndexId { get; set; }
        [Display(Name = "Example on")]
        public int JqTutorialId { get; set; }

        public bool ActiveStatus { get; set; }
        public int AdminId { get; set; }
        public string Date { get; set; }
    }
}