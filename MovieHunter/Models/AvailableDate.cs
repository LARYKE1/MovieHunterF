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


        [ForeignKey(nameof(MovieId))]
        public virtual Movies Movie { get; set; }

        [InverseProperty("Date")]
        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}
