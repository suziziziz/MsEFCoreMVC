using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

using MsEFCoreMVC.Models;
using MsEFCoreMVC.Models.SchoolViewModels;
using MsEFCoreMVC.Data;

namespace MsEFCoreMVC.Controllers;

public class HomeController : Controller {
  private readonly ILogger<HomeController> _logger;
  private readonly SchoolContext _context;

  public HomeController(ILogger<HomeController> logger, SchoolContext context) {
    _logger = logger;
    _context = context;
  }

  public IActionResult Index() {
    Console.WriteLine("Readed");
    return View();
  }

  public async Task<IActionResult> About() {
    IQueryable<EnrollmentDateGroup> data =
      from student in _context.Students
      group student by student.EnrollmentDate into dateGroup
      select new EnrollmentDateGroup() {
        EnrollmentDate = dateGroup.Key,
        StudentCount = dateGroup.Count(),
      };
    return View(await data.AsNoTracking().ToListAsync());
  }

  public IActionResult Privacy() {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error() {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
