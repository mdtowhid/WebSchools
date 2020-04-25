using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models.BlogVm
{
    [Table("BlogContents")]
    public class BlogContent
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Blog Heading")]
        public string BlogHeading { get; set; }

        //[Required]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Blog Description")]
        public string BlogDesc { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool ActiveStatus { get; set; }
        
        public int BloggerId { get; set; }

        [Required]
        [Display(Name = "Blog Categories")]
        public int BlogCategoryId { get; set; }

        public virtual BlogCategory BlogCategory { get; set; }

        public string PostedBy { get; set; }

        public string Slug { get; set; }

        public int Sort { get; set; }
    }
}