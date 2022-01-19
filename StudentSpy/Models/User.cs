using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StudentSpy.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name should be provided")]
        public string Name { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name should be provided")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Email is not valid")]
        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password should be provided")]
        public string Password { get; set; }

        [DisplayName("Register Date")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        [DisplayName("Study Date")]
        public DateTime StudyDate { get; set; }
        public string PhotoPath { get; set; }
    }
}
