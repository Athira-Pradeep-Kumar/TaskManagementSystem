using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace TaskManagementSystem.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "The password must contain at least one lowercase letter, one uppercase letter, and one number.")]
        public string Password { get; set; }

        [ForeignKey("Roles")]
        public int RoleId { get; set; }

        public virtual Roles Roles { get; set; }


    }
}