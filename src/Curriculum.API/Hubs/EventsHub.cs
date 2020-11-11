using System;
using System.Threading.Tasks;
using Curriculum.API.Data.Models;
using Curriculum.API.Hubs.Groups;
using Identity.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Curriculum.API.Hubs
{
    [Authorize]
    public class EventsHub : Hub<IEventsHub>
    {
        private readonly ILogger<EventsHub> _logger;

        public EventsHub(ILogger<EventsHub> logger)
        {
            _logger = logger;
        }

        public async Task JoinToGroup(GroupType groupType, int id)
        {
            _logger.LogInformation("user {0} joined", Context.UserIdentifier);
            _logger.LogInformation("user detail : ", Context.User.GetUserId());
            await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(groupType, id));
        }

        public async Task LeftFromGroup(GroupType groupType, int id)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(groupType, id));
        }

        public static string GetGroupName(GroupType type, int id)
        {
            return type switch
            {
                GroupType.Field => "field_" + id,
                _ => throw new Exception("group not found")
            };
        }
    }

    public interface IEventsHub
    {
        Task CurriculumUpdated(int id, Data.Models.Curriculum curriculum);
        Task CurriculumFreed(int id, int remainingCapacity);
        Task CurriculumCompleted(int id);
        Task StudentCurriculumAdded(int id, StudentCurriculumStatus status, string description);
        Task StudentCurriculumRemoved(int id);
        Task PrivateMessage(string message);
    }
}