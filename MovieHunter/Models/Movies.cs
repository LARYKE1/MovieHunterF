using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

#nullable disable

namespace MovieHunter.Models
{
    public class Movies
    {
        [Key]
        public int MovieId { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(516)]
        public string Description { get; set; }
        public int? Duration { get; set; }
        [StringLength(20)]
        public string Genre { get; set; }
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy}")]
        public DateTime? ReleaseDate { get; set; }
        public string PictureUrl { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal PricePerTicket { get; set; }

        [InverseProperty(nameof(Reservation.Movie))]
        public virtual ICollection<Reservation> Reservations { get; set; }

        public ICollection<AvailableDate> AvailableDate { get; set; }

        

    }
}
