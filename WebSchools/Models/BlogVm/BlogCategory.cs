using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models.BlogVm
{
    [Table("BlogCategories")]
    public class BlogCategory
    {
        public int Id { get; set; }
        public string BlogCategoryName { get; set; }
        public bool ActiveStatus { get; set; }
    }
}