using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.SignalR;

namespace Curriculum.API.Configurations
{
    public class NameUserIdIdentifier : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                       .Value ??
                   connection.User?.FindFirst("sub").Value;
        }
    }
}