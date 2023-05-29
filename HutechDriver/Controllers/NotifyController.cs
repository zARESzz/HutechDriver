using HutechDriver.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HutechDriver.Controllers
{
    public class NotifyController : Controller
    {
        // GET: Notify
        private readonly IHubContext<TripHub> _hubContext;

        public NotifyController(IHubContext<TripHub> hubContext)
        {
             _hubContext = hubContext;
        }

        public async Task<IActionResult> NotifyPassenger(string passengerId)
        {
            await _hubContext.Clients.User(passengerId).SendNotificationToPassenger(passengerId);
            return (IActionResult)Json(new { success = true });
        }
    }
}
