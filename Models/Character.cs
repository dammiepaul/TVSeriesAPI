using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models
{
    public class Character
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Status { get; set; }

        public string StateOfOrigin { get; set; }

        [Required]
        public string Gender { get; set; }

        public Location Location { get; set; }          //1:1 between Character and Location
        public List<Episode> Episodes { get; set; }     //*:* between Character and Episodes

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Modified { get; set; }
    }
}
