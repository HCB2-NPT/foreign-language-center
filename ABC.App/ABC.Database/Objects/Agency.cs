using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Database.Objects
{
    [Table("Agency")]
    public class Agency
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Non-Nullable!"), MaxLength(256)]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Non-Nullable!"), MaxLength(16)]
        public string PhoneNumber { get; set; }
    } 
}
