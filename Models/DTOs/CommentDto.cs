using System;
using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public string IpAddressLocation { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public int EpisodeId { get; set; }
        public string EpisodeName { get; set; }
    }
}
