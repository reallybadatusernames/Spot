using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Diagnostics;
using System.Threading.Tasks;

using MediatR;

using Spot.Models;
using Spot.Infrastructure.Command.Fetches;
using Spot.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using Spot.Services;
using System.Linq;

namespace Spot.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IMediator _mediatr;

        private IServiceProvider _provider;

        public HomeController(ILogger<HomeController> logger, IMediator mediator, IServiceProvider provider)
        {
            _logger = logger;
            _mediatr = mediator;
            _provider = provider;
        }

        public async Task<IActionResult> Index()
        {
            //var fetches = await _mediatr.Send(new GetFetches.Query { });
            //var service = _provider.GetRequiredService<QuartzService>();
            //await Task.Delay(120000);
            //fetches.Fetches.FirstOrDefault().AbsolutePath = "/play/draws";
            //await service.UpdateJob(fetches.Fetches.First());

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
