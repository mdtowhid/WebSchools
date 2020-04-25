using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSchools.Models.Css
{
    [Table("CssTutorials")]
    //[Bind(Include = "Heading, Explanation, CssProperties, Example")]
    public class CssTutorial : BaseTutorialData
    {
        [Required]
        public string CssPropertyHeadingName { get; set; }
        [Required]
        public string CssProperties { get; set; }
    }
}