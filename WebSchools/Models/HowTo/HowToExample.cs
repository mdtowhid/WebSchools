using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models.HowTo
{
    [Table("HowToExamples")]
    public class HowToExample : BaseTutorialExample
    {
        [Key]
        public int Id { get; set; }
        public string Step { get; set; }
        public string ExampleHeading { get; set; }
        public string ExampleCode { get; set; }
        public string Slug { get; set; }
    }
}