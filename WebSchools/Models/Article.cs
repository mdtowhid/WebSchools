using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models
{
    [Table("Articles")]
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Article Title")]
        [Required(ErrorMessage = "Title must be required.")]
        public string ArticleTitle { get; set; }

        [Display(Name = "Main Article")]
        [Required(ErrorMessage = "Please insert desired Article.")]
        public string MainArticle { get; set; }

        [Display(Name = "Article Explanation")]
        [Required(ErrorMessage = "Please field out this field.")]
        public string ArticleExplanation { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please pickup date.")]
        public DateTime Date { get; set; }

        [Display(Name = "RegistrationId")]
        public int RegistrationId { get; set; }
        public virtual Registration Registration { get; set; }

        [Display(Name = "Languages")]
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }

        public string CreatedBy { get; set; }

        public bool ActiveStatus { get; set; }
    }
}