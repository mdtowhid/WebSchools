using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models.Php
{
    [Table("PhpTutorials")]
    public class PhpTutorial
    {
        public int Id { get; set; }
        [Required]
        public string SubHeading{ get; set; }
        [Required]
        public string HeadingDescription { get; set; }
        [Required]
        public string Example { get; set; }
        [Required]
        public string Slug { get; set; }
        public bool ActiveStatus { get; set; }

        public int AdminId { get; set; }
        public string Date { get; set; }
    }
}