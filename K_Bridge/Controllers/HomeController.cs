using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using K_Bridge.Models;
using Microsoft.AspNetCore.Identity;

namespace K_Bridge.Controllers;

public class HomeController : Controller
{

    private IKBridgeRepository repository;

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public HomeController(IKBridgeRepository repo) { repository = repo; }

    public IActionResult Index()
    {
        ViewBag.Statses = repository.Statses;
        ViewBag.Categories = repository.Categories;
        ViewBag.Topics = repository.Topics;
        return View();
    }
}
//dotnet ef migrations add Initial