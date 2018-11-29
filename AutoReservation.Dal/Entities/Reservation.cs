using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Reservationen")]
    public class Reservation {

        [Key, Column("Id")]
        public int ReservationsNr { get; set; }

        [Column("Auto")]
        public int AutoId { get; set; }

        [Column("Kunde")]
        public int KundeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Von { get; set; }

        [DataType(DataType.Date)]
        public DateTime Bis { get; set; }

        [ForeignKey("AutoId"), InverseProperty("Reservationen")]
        public virtual Auto Auto { get; set; }

        [ForeignKey("KundeId"), InverseProperty("Reservationen")]
        public virtual Kunde Kunde { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
