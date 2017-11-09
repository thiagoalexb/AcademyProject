using Academy.Domain.Entities.Core;
using System;
using System.Collections.Generic;

namespace Academy.Domain.Entities
{
    public class Post : Entity
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string MainImage { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
