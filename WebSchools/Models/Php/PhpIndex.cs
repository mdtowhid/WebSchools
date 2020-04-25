using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models.Php
{
    [Table("PhpIndices")]
    public class PhpIndex
    {
        public int Id { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string HeadingDescription { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public int Sort { get; set; }
        [Required]
        public bool ActiveStatus { get; set; }
    }
}