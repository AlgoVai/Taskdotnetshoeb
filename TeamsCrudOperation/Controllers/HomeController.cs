using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TeamsCrudOperation.Data;
using TeamsCrudOperation.Helper;
using TeamsCrudOperation.Models;

namespace TeamsCrudOperation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> CreateTeam()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam(TeamInfo teamInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newData = new TeamInfo
                    {
                        TeamName = teamInfo.TeamName,
                        Description = teamInfo.Description,
                        ApprovedByDirector = 0,
                        ApprovedByManager = 0,


                    };
                    _context.TeamInfos.Add(newData);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("GetTeamInfo", "Home");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    ModelState.AddModelError("", "An error occurred while saving the data.");
                    TempData["error"] = "An error occurred while saving the data.";
                    return View(teamInfo);
                }
            }
            else
            {
                TempData["error"] = "Data cannot be submitted due to validation errors.";
                return View(teamInfo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamInfo()
        {
            try
            {
                var data = await Task.FromResult(_context.TeamInfos.ToList());
                return View(data);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditTeam(int id)
        {
            var data = await Task.FromResult(_context.TeamInfos.
                                            Where(x => x.TeamId == id).
                                            FirstOrDefault());
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditTeam(TeamInfo obj)
        {
            var data = await Task.FromResult(_context.TeamInfos.
                                            Where(x => x.TeamId == obj.TeamId).
                                            FirstOrDefault());
            if (data == null)
            {
                throw new Exception("Data not found");
            }
            data.TeamName = obj.TeamName;
            data.Description = obj.Description;
            _context.TeamInfos.Update(data);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetTeamInfo", "Home");

        }
        [HttpGet]
        public async Task<IActionResult> DeleteTeam(int id)
        {

            var data = await Task.FromResult(_context.TeamInfos.Where(y => y.TeamId == id).FirstOrDefault());

            if (data == null)
            {
                throw new Exception("Data not found");
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTeam(TeamInfo obj)
        {
            try
            {
                var data = await Task.FromResult(_context.TeamInfos.Where(y => y.TeamId == obj.TeamId).FirstOrDefault());


                if (data == null)
                {
                    throw new Exception("Data not found");
                }
                _context.TeamInfos.Remove(data);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetTeamInfo", "Home");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //[HttpGet]

        //public async Task<IActionResult> SearchItem()
        //{
        //    return View();
        //}
        [HttpGet]

        public async Task<IActionResult> SearchItem(string text)
        {
            try
            {
                var data = await Task.FromResult(_context.TeamInfos.Where(y => y.TeamName.Contains(text)).ToList());
                if (data == null)
                {
                    throw new Exception("Data not found");

                }
                return View(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<MessageHelper> UpdateApprovalStatus()
        {

            return new MessageHelper
            {
                Message = "successfull update",
                statusCode = 200,
            };
        }

        [HttpPost]

        
        public async Task<MessageHelper> UpdateApprovalStatus(int teamId, int approved,int flag)
        {
            try
            {
                var data = await Task.FromResult(_context.TeamInfos.Where(y => y.TeamId == teamId).FirstOrDefault());

                if (data != null && flag==0)
                {
                   data.ApprovedByManager = approved;
                    _context.TeamInfos.Update(data);
                    await _context.SaveChangesAsync();
                }
                if (data != null && flag == 1)
                {
                    data.ApprovedByDirector = approved;
                    _context.TeamInfos.Update(data);
                    await _context.SaveChangesAsync();
                }
                return new MessageHelper
                {
                    Message = "successfull update",
                    statusCode = 200
                };
            } catch (Exception ex)
            {
                throw ex;
            }
        }








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}