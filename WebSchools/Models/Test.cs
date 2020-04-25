using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models
{
    [Table("Tests")]
    public class Test
    {
        public int Id { get; set; }
        public string Tag { get; set; }
    }
}