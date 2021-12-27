using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaransiBank.Infrastructure.Notification
{
    public interface INotificationService
    {
        Task<Task> Send(NotificationRequestModel notif);
    }
}
