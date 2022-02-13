using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace StudentSpy.Core
{
    public class User: IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name should be provided")]
        public string Name { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name should be provided")]
        public string LastName { get; set; }

        [DisplayName("Age")]
        [Required(ErrorMessage = "Age should be provided")]
        public int Age { get; set; }

        [EmailAddress(ErrorMessage = "Email is not valid")]
        [Required]
        public string Email { get; set; }

        [DisplayName("Register Date")]
        public DateTime RegisterDate { get; set; }

        [DisplayName("Study Date")]
        public DateTime StudyDate { get; set; }
        public string PhotoPath { get; set; }
    }
}