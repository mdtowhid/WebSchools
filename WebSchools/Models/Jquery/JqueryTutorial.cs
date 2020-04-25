using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebSchools.Models.HowTo;

namespace WebSchools.Models.Jquery
{
    [Table("JqueryTutorials")]
    public class JqueryTutorial
    {
        public int Id { get; set; }
        [Required]
        public string SubHeading { get; set; }
        [Required]
        public string SubExplanation { get; set; }
        [Required]
        public string CodeSample { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public int Sort { get; set; }
        [Required]
        public bool ActiveStatus { get; set; }
        [Required]
        public string AwesomeClass { get; set; }
        [Required]
        public int JqueryIndexId { get; set; }


        public int AdminId { get; set; }
        public string Date { get; set; }
    }
}