using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Database.Objects
{
    [Table("TestSchedule")]
    public class TestSchedule
    {
        [Key, StringLength(10)]
        public string TestScheduleId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Non-Nullable!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Non-Nullable!")]
        public string AgencyId { get; set; }

        [Required(ErrorMessage = "Non-Nullable!")]
        public string CertificateId { get; set; }

        [ForeignKey("AgencyId")]
        public Agency Agency { get; set; }

        [ForeignKey("CertificateId")]
        public Certificate Certificate { get; set; }
    }
}
