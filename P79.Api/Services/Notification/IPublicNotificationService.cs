using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaransiBank.Infrastructure.Notification
{
    public interface IPublicNotificationService
    {
        Task Send(NotificationRequestModel notif);
        Task Send<T>(NotificationRequestModel<T> notif);
        Task Send(NotificationRequestModel<object> notif);
    }
}
