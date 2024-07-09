﻿using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Controllers
{
    public class CateController : Controller
    {
        [Route("/Categories")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Categories/TopicList")]
        public IActionResult TopicList()
        {
            return View();
        }

        [Route("/Categories/TopicList/Post/{postId}/{postTitle}")]
        public IActionResult Post(int postId, string postTitle)
        {
            postId = 123;
            postTitle = "my-first-post";
            return View();
        }
    }
}
