using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Lab6.Models
{
    public class Student
    {

        [SwaggerSchema(ReadOnly = true)]
        public Guid Id { get; set; }

        [Required]
        [DisplayName("FirstName")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("LastName")]
        [StringLength(50)]
        public string LastName { get; set; }

        [DisplayName("Program")]
        public string Program { get; set; }
    }
}
