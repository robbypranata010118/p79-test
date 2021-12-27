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
    public class NotificationService: INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationService(IHubContext<NotificationHub> hubContext) {
            //_notificationServiceSettings = notificationServiceSettings;
            //client.BaseAddress = new Uri(_notificationServiceSettings.Url);
            //_client = client;
            _hubContext = hubContext;
        }
        public async Task<Task> Send(NotificationRequestModel notif)
        {
            //if (notif.NotifyPublic) {
            //    var response = await _NotifyPublic(notif);
            //    if (response.StatusCode != System.Net.HttpStatusCode.OK) {
            //        throw new Exception("Failed to notify public endpoint");
            //    }
            //}
            return _hubContext.Clients.All.SendAsync(notif.EventName, JsonConvert.SerializeObject(notif));
        }
        //private async Task<HttpResponseMessage> _NotifyPublic(NotificationRequestModel notif) {
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(notif), Encoding.UTF8, "application/json");
        //    var response =  await _client.PostAsync(_notificationServiceSettings.EndPoint, content);
        //    response.EnsureSuccessStatusCode();
        //    return response;
        //}
    }
}
