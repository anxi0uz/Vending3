using backend.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using vendingbackend.Core.DTOs;

namespace backend.Endpoints
{
    public static class TradeApparatusEndpoints
    {
        public static IEndpointRouteBuilder MapTradeApparatuses(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/trade-apparatus").RequireAuthorization();

            group.MapGet("/", async (ITradeApparatusRepository repository, INotificationSender sender) =>
            {
                try
                {
                    return await repository.GetAllTradesAsync();
                }
                catch (Exception ex)
                {
                    var dto = new NotificationDto(0, "Невозможно получить список", DateTime.Now);
                    await sender.SendNotificationAsync(dto);
                    return null;
                }
            });

            group.MapGet("/{id}", async (ITradeApparatusRepository repository, int id, INotificationSender sender) =>
            {
                try
                {
                    return await repository.GetTradeApparatusByIdAsync(id);
                }
                catch (Exception ex)
                {
                    var dto = new NotificationDto(0, "Невозможно получить аппарат", DateTime.Now);
                    await sender.SendNotificationAsync(dto);
                    return null;
                }
            });

            group.MapPut("/{id}", async (ITradeApparatusRepository repository, int id, [FromBody] TradeApparatusRequest request, INotificationSender sender) =>
            {
                try
                {
                    return await repository.UpdateTradeApparatusAsync(id, request);
                }
                catch (Exception ex)
                {
                    var dto = new NotificationDto(0, "Невозможно обновить аппарат", DateTime.Now);
                    await sender.SendNotificationAsync(dto);
                    return 0;
                }
            });

            group.MapPost("/", async (ITradeApparatusRepository repository, [FromBody] TradeApparatusRequest request, INotificationSender sender) =>
            {
                try
                {
                    return await repository.CreateTradeAsync(request);
                }
                catch (Exception ex)
                {
                    var dto = new NotificationDto(0, "Невозможно создать аппарат", DateTime.Now);
                    await sender.SendNotificationAsync(dto);
                    return 0;
                }
            });

            group.MapDelete("/{id}", async (ITradeApparatusRepository repository, int id, INotificationSender sender) =>
            {
                try
                {
                    return await repository.DeleteTradeApparatusAsync(id);
                }
                catch (Exception ex)
                {
                    var dto = new NotificationDto(0, "Невозможно удалить аппарат", DateTime.Now);
                    await sender.SendNotificationAsync(dto);
                    return 0;
                }

            });

            return group;
        }
    }
}
