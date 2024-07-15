﻿using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Controllers
{
    public class UserController : Controller
    {
        [Route("/UserProfile/{UserID}")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/UserProfile/EditProfile/{UserID}")]
        public IActionResult EditProfile()
        {
            return View();
        }
    }
}