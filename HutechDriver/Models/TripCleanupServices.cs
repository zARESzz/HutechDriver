using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using Microsoft.Extensions.Hosting;

namespace HutechDriver.Models
{
    public class TripCleanupServices : BackgroundService
    {
        private readonly TimeSpan cleanupInterval = TimeSpan.FromHours(1);
        private readonly ApplicationDbContext dbContext;

        public TripCleanupServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CleanupTripsAsync();
                await Task.Delay(cleanupInterval, stoppingToken);
            }
        }

        private async Task CleanupTripsAsync()
        {
            DateTime threshold = DateTime.Now.AddHours(-2);
            var tripsToRemove = dbContext.Trips
                .Where(t => t.Status == "Chưa nhận" && t.OrderDate < threshold)
                .ToList();

            dbContext.Trips.RemoveRange(tripsToRemove);
            await dbContext.SaveChangesAsync();
        }
    }
}