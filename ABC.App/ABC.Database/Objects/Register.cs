using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Database.Objects
{
    [Table("Register")]
    public class Register
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Non-Nullable!")]
        public DateTime DateReg { get; set; }

        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
        public double TestScore { get; set; }

        [Required(ErrorMessage = "Non-Nullable!")]
        public string StudentId { get; set; }

        [Required(ErrorMessage = "Non-Nullable!")]
        public string TestScheduleId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [ForeignKey("TestScheduleId")]
        public TestSchedule TestSchedule { get; set; }
    }
}
