using vendingbackend.Core.DTOs;

namespace backend.Abstractions
{
    public interface INotificationSender
    {
        Task SendNotificationAsync(NotificationDto dto);
    }
}