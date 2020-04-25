using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models.JavaScript
{
    [Table("SqlSyntaxBoxes")]
    public class SqlSyntaxBox
    {
        public int Id { get; set; }
        [Required]
        public string SubHeading { get; set; }
        [Required]
        public string SqlSyntax { get; set; }
        [Required]
        public string SqlSyntaxDescription { get; set; }
        [Required]
        public int SqlIndexId { get; set; }
        [Required]
        public string Slug { get; set; }
        [Display(Name = "Example On ")]
        public int SqlExampleId { get; set; }

        public bool ActiveStatus { get; set; }

        public int AdminId { get; set; }

        public string Date { get; set; }
    }
}