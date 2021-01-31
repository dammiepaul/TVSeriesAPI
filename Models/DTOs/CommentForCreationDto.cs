using System;
using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.DTOs
{
    public class CommentForCreationDto
    {

        [Required]
        [StringLength(249)]
        public string CommentText { get; set; }

        [Required]
        public string IpAddressLocation { get; set; }
    }
}
