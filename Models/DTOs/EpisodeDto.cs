using System;


namespace TVSeriesAPI.Models.DTOs
{
    public class EpisodeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string EpisodeCode { get; set; }
        public int NumberOfComments { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
