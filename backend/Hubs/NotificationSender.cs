using backend.Abstractions;
using backend.DataAccess;
using backend.Models;
using Microsoft.AspNetCore.SignalR;
using vendingbackend.Core.DTOs;

namespace backend.Hubs
{
    public class NotificationSender : INotificationSender
    {
        private readonly IHubContext<NotificationHub> hubContext;
        private readonly AppDbContext _dbContext;

        public NotificationSender(IHubContext<NotificationHub> hubContext, AppDbContext dbContext)
        {
            this.hubContext = hubContext;
            this._dbContext = dbContext;
        }

        public async Task SendNotificationAsync(NotificationDto dto)
        {
            var log = new NotificationLog()
            {
                Date = dto.Date,
                Message = dto.Message,
                Type = (NotificationType)dto.Type,
            };
            await _dbContext.NotificationLogs.AddAsync(log);
            await _dbContext.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("TaNotification", dto);
        }
    }
}
