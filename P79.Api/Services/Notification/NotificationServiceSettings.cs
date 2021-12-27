using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaransiBank.Infrastructure.Notification
{
    public class NotificationServiceSettings
    {
        public string Url { get; set; }
        public bool IsPublic { get; set; }
        public string EndPoint { get; set; }
    }
}
