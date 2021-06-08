using MediatR;

using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Spot.Domain;
using Spot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Spot.Infrastructure.Command.Fetches
{
    public class GetFetches
    {
        public class Query : IRequest<Result>
        {
            
        }

        public class Result
        {
            public IEnumerable<Fetch> Fetches { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly SqlContext _ctx;

            public Handler(SqlContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                return new Result 
                {  
                    Fetches = await _ctx.Fetches.Include(x => x.Site).ToListAsync() 
                };
            }
        }
    }
}
