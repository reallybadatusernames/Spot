using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spot.Domain.Entities
{
    public class Fetch
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        [ForeignKey("SiteId")]
        public Site Site { get; set; }

        public string AbsolutePath { get; set; }

        public bool AllowRedirect { get; set; }

        public bool Enabled { get; set; }
    }
}
