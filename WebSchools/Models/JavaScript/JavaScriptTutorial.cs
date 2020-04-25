using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models.JavaScript
{
    [Table("JavaScriptTutorials")]
    public class JavaScriptTutorial
    {
        public int Id { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Explanation { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public int Sort { get; set; }
        [Required]
        public bool ActiveStatus { get; set; }
        [Required]
        public string AwesomeClass { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public int AdminId { get; set; }

        public int JavaScriptIndexId { get; set; }

        public virtual JavaScriptIndex JavaScriptIndex { get; set; }
    }
}