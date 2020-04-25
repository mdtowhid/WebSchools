using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebSchools.Models.BlogVm;
using WebSchools.Models.Css;
using WebSchools.Models.HowTo;
using WebSchools.Models.Html;
using WebSchools.Models.JavaScript;
using WebSchools.Models.Jquery;
using WebSchools.Models.Php;
using WebSchools.Models.Sql;

namespace WebSchools.Models
{
    public class WebSchoolsDbContext : DbContext
    {
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleWriter> ArticleWriters { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<CssIndex> CssIndices { get; set; }
        public DbSet<CssTutorial> CssTutorials { get; set; }
        public DbSet<HtmlIndex> HtmlIndices { get; set; }
        public DbSet<HtmlTutorial> HtmlTutorials { get; set; }
        public DbSet<JavaScriptIndex> JavaScriptIndices { get; set; }
        public DbSet<JavaScriptExample> JavaScriptExamples { get; set; }
        public DbSet<JavaScriptTutorial> JavaScriptTutorials { get; set; }
        public DbSet<HowToIndex> HowToIndices { get; set; }
        public DbSet<HowToExample> HowToExamples { get; set; }
        public DbSet<HowToTutorial> HowToTutorials { get; set; }
        public DbSet<JqueryIndex> JqueryIndices { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<JqueryTutorial> JqueryTutorials { get; set; }
        public DbSet<JqueryExample> JqueryExamples { get; set; }
        public DbSet<SqlIndex> SqlIndices { get; set; }
        public DbSet<SqlExample> SqlExamples { get; set; }
        public DbSet<SqlSyntaxBox> SqlSyntaxBoxes { get; set; }
        public DbSet<DemoCustomer> DemoCustomers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminCategory> AdminCategories { get; set; }
        public DbSet<PriorExperience> PriorExperiences { get; set; }
        public DbSet<PhpIndex> PhpIndices { get; set; }
        public DbSet<PhpTutorial> PhpTutorials { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogContent> BlogContents { get; set; }

        public System.Data.Entity.DbSet<WebSchools.Models.SuperAdmins> SuperAdmins { get; set; }
    }
}