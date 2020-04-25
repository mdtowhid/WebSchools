using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSchools.Models
{
    public class BaseTutorialData
    {
        public int Id { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Explanation { get; set; }
        [Required]
        public string Example { get; set; }
        [Required]
        public string Description { get; set; }
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
        public virtual Admin Admin { get; set; }

    }
}