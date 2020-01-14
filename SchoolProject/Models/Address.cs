using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    [Table("Address")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //generated Key
        public int AddressId { get; set; }

        [MinLength(10),MaxLength(50)]
        public string Address1 { get; set; }

        public string Address2 { get; set; }
        public  string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; } = "Egypt";
        public int ZippCode { get; set; }

        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }

    }
}
