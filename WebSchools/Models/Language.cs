using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models
{
    [Table("Languages")]
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string Names { get; set; }
    }
}