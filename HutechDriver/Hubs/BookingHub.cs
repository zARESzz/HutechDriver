using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HutechDriver.Hubs
{
    public class BookingHub : Hub
    {
       
        public void SendBookingUpdate()
        {
            Clients.All.updateBookings();
        }
    }
}