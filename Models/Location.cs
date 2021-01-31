using System;
using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public int CharacterId { get; set; }        //Foreign key to Character Entity

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Modified { get; set; }
    }
}
