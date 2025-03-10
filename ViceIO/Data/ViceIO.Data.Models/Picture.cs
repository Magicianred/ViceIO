﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ViceIO.Data.Common.Models;

namespace ViceIO.Data.Models
{
    public class Picture : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public int AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public ICollection<PictureVote> PictureVotes { get; set; }
            = new HashSet<PictureVote>();
    }
}
