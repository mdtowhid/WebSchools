using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models.HowTo
{
    [Table("HowToTutorials")]
    public class HowToTutorial : BaseTutorialData
    {
        public int HowToIndexId { get; set; }
        public virtual HowToIndex HowToIndex { get; set; }
        public int HowToExampleId { get; set; }
        public virtual HowToExample HowToExample { get; set; }
    }
}