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
        [Column(Order=1), Key, Required(ErrorMessage = "Non-Nullable!")]
        public string StudentId { get; set; }

        [Column(Order = 2), Key, Required(ErrorMessage = "Non-Nullable!")]
        public int TestScheduleId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        
        [ForeignKey("TestScheduleId")]
        public virtual TestSchedule DateTest { get; set; }

        [Required(ErrorMessage = "Non-Nullable!")]
        public DateTime DateReg { get; set; }

        [Required(ErrorMessage = "Non-Nullable!"), Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
        public double TestScore { get; set; }
    }
}
