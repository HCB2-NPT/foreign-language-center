using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Database.Objects
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public string PersonalId { get; set; }

        [Required(ErrorMessage = "Non-Nullable!"), MaxLength(64)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Non-Nullable!")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Non-Nullable!"), MaxLength(16)]
        public string PhoneNumber { get; set; }
    }
}
