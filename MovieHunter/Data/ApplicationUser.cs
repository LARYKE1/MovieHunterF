using Microsoft.AspNetCore.Identity;
using MovieHunter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieHunter.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        [PersonalData]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [PersonalData]
        public string LastName { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public override string? PhoneNumber { get; set; }

        [InverseProperty(nameof(Reservation.Customer))]
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}