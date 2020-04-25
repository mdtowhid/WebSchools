using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models
{
    [Table("AdminCategories")]
    public class AdminCategory
    {
        public int Id { get; set; }
        [Required]
        public string AdminType { get; set; }
        public bool ActiveStatus { get; set; }
    }
}