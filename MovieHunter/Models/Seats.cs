using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieHunter.Models
{
    public class Seats
    {
        [Key]
        public int SeatId { get; set; }
        [RegularExpression(@"^[A-Z]\d{1,2}$", ErrorMessage = "Seat number must be in the format 'A1' to 'A20'.")]
        public string SeatNumber { get; set; }
        [DefaultValue(false)]
        public bool IsReserved { get; set; }
        public int MovieId { get; set; }

        public Movies Movie { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
