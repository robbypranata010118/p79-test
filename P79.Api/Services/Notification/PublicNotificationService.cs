using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace GaransiBank.Infrastructure.Notification
{
    public class PublicNotificationService: IPublicNotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public PublicNotificationService(IHubContext<NotificationHub> hubContext) {
            _hubContext = hubContext;
        }
        public Task Send(NotificationRequestModel notif)
        {
            return _hubContext.Clients.All.SendAsync(notif.EventName, JsonConvert.SerializeObject(notif));
        }
        public Task Send<T>(NotificationRequestModel<T> notif)
        {
            return _hubContext.Clients.All.SendAsync(notif.EventName, JsonConvert.SerializeObject(notif));
        }
        public Task Send(NotificationRequestModel<object> notif)
        {
            return _hubContext.Clients.All.SendAsync(notif.EventName, JsonConvert.SerializeObject(notif));
        }
    }
}
