using MediatR;
using SigmaTest.DataAccess;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SigmaTest.Service.Features.ShortnerFeatures.Commands
{
    public class UpdateShortnerCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateShortnerCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateCustomerCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateShortnerCommand request, CancellationToken cancellationToken)
            {
                var shortner = _context.Shortner.Where(a => a.Id == request.Id).FirstOrDefault();

                if (shortner == null)
                {
                    return default;
                }
                else
                {
                    shortner.NumOfClicks ++;
                    _context.Shortner.Update(shortner);
                    await _context.SaveChangesAsync();
                    return shortner.Id;
                }
            }
        }
    }
}
