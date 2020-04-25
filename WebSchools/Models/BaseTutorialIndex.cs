using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSchools.Models
{
    public class BaseTutorialIndex
    {
        public int Id { get; set; }
        [Required]
        public string MainHeadingDescription { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public int Sort { get; set; }
        [Required]
        public string ActiveStatus { get; set; }
        public string MenuDivider { get; set; }
    }
}