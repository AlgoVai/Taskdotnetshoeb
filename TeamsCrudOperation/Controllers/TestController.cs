using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TeamsCrudOperation.Data;
using TeamsCrudOperation.Models;

namespace TeamsCrudOperation.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDBContext _context;
        public TestController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public IActionResult CreateTeam()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTeam(TeamInfo teamInfo)
        {
            if (ModelState.IsValid)
            {
                var newData = new TeamInfo
                {
                    TeamName = teamInfo.TeamName,
                    Description = teamInfo.Description,

                };
                _context.TeamInfos.Add(newData);
                _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "data can not be submitted";
                return View(teamInfo);
            }
        }
    }
}
