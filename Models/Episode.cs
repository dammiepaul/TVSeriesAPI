using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models
{
    public class Episode
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string EpisodeCode { get; set; }

        public List<Character> Characters { get; set; }     //*:* between Episodes and Characters
        public List<Comment> EpisodeComments { get; set; }   //1:* between Episodes and Comments

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Modified { get; set; }
    }
}
