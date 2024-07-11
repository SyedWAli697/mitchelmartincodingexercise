using ConsumeRedditWebAPI.Models;
using ConsumeRedditWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace ConsumeRedditWebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRedditAccountService _redditAccountService;
        private readonly IConfiguration _configuration;
        private readonly IRedditService _redditService;

        public HomeController(
            IRedditAccountService redditAccountService,
            IConfiguration configuration,
            IRedditService redditService)
        {
            _redditAccountService = redditAccountService;
            _configuration = configuration;
            _redditService = redditService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var token = await _redditAccountService.GetToken(
                    _configuration["Reddit:userName"],
                    _configuration["Reddit:userPassword"],
                    _configuration["Reddit:clientId"],
                    _configuration["Reddit:clientSecret"],
                    _configuration["Reddit:userAgent"]
                    );

                var mostUpVotes = await _redditService.GetMostUpVotes(token);
            }

            catch (Exception ex)
            {
                Debug.Write(ex);
            }

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
