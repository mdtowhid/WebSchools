using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models.JavaScript
{
    [Table("SqlExamples")]
    public class SqlExample
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Example Heading")]
        public string ExampleHeading { get; set; }

        [Required]
        [Display(Name = "Ex. Heading Desc.")]
        public string ExampleHeadingDescription { get; set; }

        [Required]
        [Display(Name = "Examle Box")]
        public string ExampleBox { get; set; }
        [Required]
        [Display(Name = "Example Desc.")]
        public string ExampleDescription { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public int SqlIndexId { get; set; }

        public bool ActiveStatus { get; set; }

        public int AdminId { get; set; }

        public string Date { get; set; }

    }
}