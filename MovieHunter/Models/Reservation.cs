using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MovieHunter.Data;

#nullable disable

namespace MovieHunter.Models
{
    public class Reservation
    {
        [Key]
        [Column("ReservationID")]
        public int ReservationId { get; set; }
        public byte TicketNumbers { get; set; }
        public DateTime ReservationTime { get; set; }
        
        public int MovieId { get; set; }
        [StringLength(450)]
        public string CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual ApplicationUser Customer { get; set;}
        [ForeignKey(nameof(MovieId))]
        [InverseProperty("Reservations")]
        public virtual Movies Movie { get; set; }

        //[ForeignKey(nameof(ReservationTime))]
        //[InverseProperty("DatesReservation")]
        //public virtual AvailableDate Dates { get; set; }

    }
}
