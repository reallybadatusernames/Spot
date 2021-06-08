using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spot.Domain.Entities
{
    public class Dig
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        [ForeignKey("SiteId")]
        public Site Site { get; set; }

        public string AbsolutePath { get; set; }

        public TimeSpan Interval { get; set; }

        public List<DigParameters> Parameters { get; set; }
    }
}
