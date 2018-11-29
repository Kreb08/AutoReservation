using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Autos")]
    public abstract class Auto {

        [Key]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Marke { get; set; }

        [Required]
        public int Tagestarif { get; set; }

        [Timestamp]
        public byte[] RowVersion{ get; set; }

        [InverseProperty("Auto")]
        public ICollection<Reservation> Reservationen { get; set; }
    }

    public class StandardAuto : Auto {

    }

    public class LuxusklasseAuto : Auto {
        [Required]
        public int Basistarif { get; set; }
    }

    public class MittelklasseAuto : Auto {

    }
}
