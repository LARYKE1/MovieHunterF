﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MovieHunter.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc.ModelBinding;

#nullable disable

namespace MovieHunter.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public byte TicketNumbers { get; set; }

        public int MovieId { get; set; }
        [StringLength(450)]
        public string CustomerId { get; set; }
        
        public int DateId { get; set; }


        [ForeignKey(nameof(CustomerId))]
        public virtual ApplicationUser Customer { get; set;}
        [ForeignKey(nameof(MovieId))]
        [InverseProperty("Reservations")]
        public virtual Movies Movie { get; set; }

        [ForeignKey(nameof(DateId))]
        public AvailableDate AvailableDate { get; set; }
        
        
        

    }
}
