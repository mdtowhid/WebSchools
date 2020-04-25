using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSchools.Models.Sql
{
    [Table("DemoCustomers")]
    public class DemoCustomer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }
}