using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace WebSchools.Models
{
    [Table("Registrations")]
    //[Bind(Include = "UserName, Email, Password, ConfirmPassword, RegistrationStatus")]
    public class Registration
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User Name must be required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email must be required")]
        [Display(Name = "Email")]
        [Remote("IsExistEmail", "Account", ErrorMessage = "Email already exists!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must be required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Minimum 6 characters required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Comfirmed password")]
        [Display(Name = "Compare Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage="Comfirm password and password do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select Checkbox")]
        public string RegistrationStatus { get; set; }
        public bool NameInUse { get; set; }
        public bool RememberMe { get; set; }
        public bool IsEmailVerified { get; set; }
        //public Guid ActivationCode { get; set; }

        //new updated ...
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public bool ActiveStatus { get; set; }
    }
}