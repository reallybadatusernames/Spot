using System;

namespace Spot.Domain.Entities
{
    public class Site
    {
        public Guid Id { get; set; }

        public bool UsesHttps { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public bool Active { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }
    }
}
