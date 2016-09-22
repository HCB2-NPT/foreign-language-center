using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Database.Objects
{
    [Table("Certificate")]
    public class Certificate
    {
        [Key]
        public string Name { get; set; }

        [Required(ErrorMessage = "Non-Nullable!")]
        public double Fee { get; set; }
    }
}
