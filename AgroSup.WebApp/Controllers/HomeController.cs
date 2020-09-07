using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IYearPlanRepository _yearPlanRepository;
        private readonly IUserRepository _userRepository;

        public HomeController(
            IUserRepository userRepository,
            ILogger<HomeController> logger,
            IYearPlanRepository yearPlanRepository,
            UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
            _yearPlanRepository = yearPlanRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            //var loggedUserId = _userManager.GetUserId(User);
            //var user = _userRepository.GetById(Guid.Parse(loggedUserId)).Result;
            //user.ManagedYearPlan = null;
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
