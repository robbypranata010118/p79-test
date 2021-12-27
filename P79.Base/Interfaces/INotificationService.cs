using UIM.Base.Dtos.Notifications;
using UIM.Base.Wrappers;
using UIM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIM.Base.Interfaces
{
    public interface INotificationService
    {
        public Task<List<Notification>> Get(GetNotificationAdminRequestVm request);
        public Task<Notification> GetById(Guid Id);
        public Task<List<Notification>> GetUnread();
        public Task<int> Post(PostNotificationReadAdminDto request);
        public Task<int> Send(NotificationRequestModel request);
        public Task<PagedInfoRepositoryResponse> GetPagedInfo(GetNotificationAdminRequestVm request);
    }
}
