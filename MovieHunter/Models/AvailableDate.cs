using System.ComponentModel.DataAnnotations;

namespace MovieHunter.Models
{
    public class AvailableDate
    {
        [Key]
        public int DateId { get; set; }
        public int MovieId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public Movies Movie { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
