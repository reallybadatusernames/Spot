using MediatR;

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Spot.Domain;
using Spot.Domain.Entities;

using FluentValidation;

namespace Spot.Infrastructure.Command.Fetches
{
    public class CreateFetchResult
    {
        public class Command : IRequest
        {
            public Guid FetchId { get; set; }

            public HttpStatusCode Code { get; set; }

            public FetchStatus Status { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly SqlContext _ctx;

            public Handler(SqlContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _ctx.FetchResults.Add(new FetchResult
                {
                    FetchId = request.FetchId,
                    ResultCode = request.Code,
                    FetchDate = DateTime.Now,
                    Status = request.Status
                });

                await _ctx.SaveChangesAsync();

                return Unit.Value;
            }
        }

        public class FetchResultValidator : AbstractValidator<CreateFetchResult.Command>
        {
            public FetchResultValidator()
            {
                RuleFor(c => c.Status).NotEqual(FetchStatus.Passed);
            }
        }
    }
}
