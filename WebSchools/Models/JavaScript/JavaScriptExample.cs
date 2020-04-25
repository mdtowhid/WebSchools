using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models.JavaScript
{
    [Table("JavaScriptExamples")]
    public class JavaScriptExample : BaseTutorialExample
    {
        public bool ActiveStatus { get; set; }

        public int JavaScriptIndexId { get; set; }
        public virtual JavaScriptIndex JavaScriptIndex { get; set; }
    }
}