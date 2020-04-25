using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebSchools.BusinessLogic
{
    public class Db
    {
        private string connectionString =
            WebConfigurationManager.ConnectionStrings["WebSchoolsDbContext"].ConnectionString;
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public SqlDataReader Reader { get; set; }

        public Db()
        {
            Connection = new SqlConnection(connectionString);
        }
    }
}