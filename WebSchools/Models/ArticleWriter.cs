using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models
{
    [Table("ArticleWriters")]
    public class ArticleWriter
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int NumberOfArticleDone { get; set; }
        public int ArticleCounter { get; set; }
        public int Balance { get; set; }
    }
}