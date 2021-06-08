using System;
using System.Net;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spot.Domain.Entities
{
    public class FetchResult
    {
        public Guid Id { get; set; }

        public Guid FetchId { get; set; }

        [ForeignKey("FetchId")]
        public Fetch Fetch { get; set; }

        public DateTime FetchDate { get; set; }

        public HttpStatusCode ResultCode { get; set; }

        public FetchStatus Status { get; set; }
    }

    public enum FetchStatus
    {
        Passed = 0,
        Error = 1,
        Inactive = 2
    }
}
