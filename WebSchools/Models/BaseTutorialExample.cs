using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSchools.Models
{
    public class BaseTutorialExample
    {
        public int Id { get; set; }
        public string SubHeading { get; set; }
        public string SubDescription { get; set; }
        public string Description { get; set; }
        public string Example { get; set; }
        public int AdminId { get; set; }
        public string Date { get; set; }
        public string Slug { get; set; }
    }
}