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
        [Key, StringLength(10)]
        public string PersonalId { get; set; }

        [Required(ErrorMessage = "Non-Nullable!"), StringLength(64)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Non-Nullable!")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Non-Nullable!"), StringLength(16)]
        public string PhoneNumber { get; set; }
    }
}
