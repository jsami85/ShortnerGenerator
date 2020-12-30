using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SigmaTest.Infrastructure.ViewModel;
using SigmaTest.Service.Contract;
using SigmaTest.Service.Features.ShortnerFeatures.Commands;
using SigmaTest.Service.Features.ShortnerFeatures.Queries;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SigmaTest.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/Shortner")]
    [ApiVersion("1.0")]
    public class ShortnerController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private readonly IHttpContextAccessor _httpContextAccessor;
        private string ip;

        public ShortnerController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UrlModel input)
        {
            if (ModelState.IsValid)
            {
                return Ok(await Mediator.Send(new CreateShortnerCommand { LongUrl = input.Url, Ip = ip }));
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("{url}")]
        public async Task<IActionResult> GetByUrl(string url)
        {
            var shortner = await Mediator.Send(new GetShortnerByUrlQuery { Url = url, Ip = ip });

            await Mediator.Send(new UpdateShortnerCommand { Id = shortner.Id });

            return new ContentResult{Content =  shortner.LongUrl, StatusCode = (int)HttpStatusCode.Moved};
        }
    }
}
