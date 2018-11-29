using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Kunden")]
    public class Kunde {

        [Key]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Vorname { get; set; }

        [Required, StringLength(20)]
        public string Nachname { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime Geburtsdatum { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [InverseProperty("Kunde")]
        public ICollection<Reservation> Reservationen { get; set; }
    }

}
