using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HutechDriver.Models
{
    public class TripHub : Hub
    {
        public Task SendNotificationToPassenger(string passengerId)
        {
             return Clients.User(passengerId).receiveMessage("Đơn của bạn đã được nhận");
        }
    }
}