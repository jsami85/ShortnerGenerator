using MediatR;
using SigmaTest.DataAccess;
using SigmaTest.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SigmaTest.Service.Features.ShortnerFeatures.Queries
{
    public class GetShortnerByUrlQuery : IRequest<Shortner>
    {
        public string Url { get; set; }
        public string Ip { get; set; }
        public class GetShortnerByUrlQueryHandler : IRequestHandler<GetShortnerByUrlQuery, Shortner>
        {
            private readonly IApplicationDbContext _context;
            public GetShortnerByUrlQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Shortner> Handle(GetShortnerByUrlQuery request, CancellationToken cancellationToken)
            {
                var customer = _context.Shortner.Where(a => a.ShortenedUrl == request.Url && a.Ip==request.Ip).FirstOrDefault();
                //customer.NumOfClicks++;
                if (customer == null) return null;
                return customer;
            }
        }
    }
}
