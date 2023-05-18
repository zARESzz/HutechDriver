namespace HutechDriver.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Trip")]
    public partial class Trip
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }

        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        [StringLength(50)]
        public string Distance { get; set; }

        [StringLength(50)]
        public string Time { get; set; }

        public decimal? Price { get; set; }

        public DateTime? TimeBook { get; set; }

        public DateTime? OrderDate { get; set; }

        public string DriverBook { get; set; }

        public string Status { get; set; }

        public string DriverId { get; set; }

        public bool? IsPaid { get; set; }

        public ICollection<TripReview> TripReviews { get; set; }

    }
}
