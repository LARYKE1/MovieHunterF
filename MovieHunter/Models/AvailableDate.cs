using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieHunter.Models
{
    public class AvailableDate
    {
        [Key]
        public int DateId { get; set; }
        [Required]
        public int MovieId { get; set; }
        [DisplayFormat(DataFormatString ="{en-US}")]
        [DataType(DataType.DateTime)]
        public DateTime? Date { get; set; }



        //[ForeignKey(nameof(MovieId))]
        //[InverseProperty("AvailableDates")]
        //public virtual Movies Movie123 { get; set; }



        //[InverseProperty(nameof(Reservation.Dates))]
        //public virtual ICollection<Reservation> DatesReservation { get; set; }

    }
}
