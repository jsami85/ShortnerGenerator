using MediatR;
using SigmaTest.Domain.Entities;
using SigmaTest.DataAccess;
using SigmaTest.Service.Contract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SigmaTest.Service.Features.ShortnerFeatures.Commands
{
    public class CreateShortnerCommand : IRequest<string>
    {
        public string LongUrl { get; set; }
        //public string ShortenedUrl { get; set; }
        public string Ip { get; set; }
        
        public class CreateShortnerCommandHandler : IRequestHandler<CreateShortnerCommand, string>
        {
            private readonly IApplicationDbContext _context;
            private IUrlManagerService _urlManager;

            public CreateShortnerCommandHandler(IApplicationDbContext context, IUrlManagerService urlManager)
            {
                _context = context;
                _urlManager = urlManager;
            }
            public async Task<string> Handle(CreateShortnerCommand request, CancellationToken cancellationToken)
            {
                var shortenedUrl = await this._urlManager.ShortenUrl(request.LongUrl, request.Ip);

                var shortner = new Shortner
                {
                    CreationDate = DateTime.Now,
                    LongUrl = request.LongUrl,
                    ShortenedUrl = shortenedUrl,
                    Ip = request.Ip,
                    NumOfClicks = 0
                };

                _context.Shortner.Add(shortner);
                await _context.SaveChangesAsync();
                return shortner.ShortenedUrl;
            }
        }
    }
}
