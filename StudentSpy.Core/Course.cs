using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSpy.Core
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name should be provided")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description should be provided")]
        public string Description { get; set; }
        [Required]
        public int Duration { get; set; }
        public string PhotoPath { get; set; }
    }
}
