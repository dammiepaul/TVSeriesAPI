using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TVSeriesAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(249)]
        public string CommentText { get; set; }

        [Required]
        public string IpAddressLocation { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Modified { get; set; }

        public int EpisodeId { get; set; }          //Foreign key to Episode Entity
        public Episode Episode { get; set; }        //1:* between Comment and Episode
    }
}
