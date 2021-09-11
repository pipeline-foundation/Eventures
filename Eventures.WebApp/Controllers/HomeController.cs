﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using Eventures.Data;
using Eventures.WebApp.Models;
using System.Security.Claims;
using System.Linq;

namespace Eventures.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        public IActionResult Index()
        {
            var userEventsCount = -1;

            if (this.User.Identity.IsAuthenticated)
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                userEventsCount = this.dbContext.Events.Where(e => e.OwnerId == currentUserId).Count();
            }

            var homeModel = new HomeViewModel()
            {
                AllEventsCount = this.dbContext.Events.Count(),
                UserEventsCount = userEventsCount
            };

            return View(homeModel);
        }

        public IActionResult Error()
            => View();

        public IActionResult Error401()
            => View();
    }
}
