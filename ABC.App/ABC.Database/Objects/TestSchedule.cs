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
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Non-Nullable!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Non-Nullable!")]
        public int AgencyId { get; set; }

        [Required(ErrorMessage = "Non-Nullable!")]
        public string CertificateId { get; set; }

        [ForeignKey("AgencyId")]
        public virtual Agency Agency { get; set; }

        [ForeignKey("CertificateId")]
        public virtual Certificate Certificate { get; set; }
    }
}
