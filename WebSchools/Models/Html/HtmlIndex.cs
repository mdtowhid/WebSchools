using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models.Html
{
    [Table("HtmlIndices")]
    public class HtmlIndex
    {
        public int Id { get; set; }
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