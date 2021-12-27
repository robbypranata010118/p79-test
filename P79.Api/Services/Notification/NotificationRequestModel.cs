using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaransiBank.Infrastructure.Notification
{
    public class NotificationRequestModel: NotificationRequestModel<string>
    {
    }
    public class NotificationRequestModel<T>
    {
        public string EventName { get; set; }
        public string ReferenceCode { get; set; }
        public T Parameter { get; set; }
        public bool NotifyPublic { get; set; } = false;
    }
}
