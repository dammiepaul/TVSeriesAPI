using System;
using System.Collections.Generic;

namespace TVSeriesAPI.Models.DTOs
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string StateOfOrigin { get; set; }
        public string Gender { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public LocationDto Location { get; set; }
        public List<EpisodeDto> Episodes { get; set; }
    }
}
