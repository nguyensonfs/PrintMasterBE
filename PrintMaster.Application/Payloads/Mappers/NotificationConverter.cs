using PrintMaster.Application.Payloads.ResponseModels.DataNotification;
using PrintMaster.Domain.Entities;

namespace PrintMaster.Application.Payloads.Mappers
{
    public class NotificationConverter
    {
        public DataResponseNotification EntityToDTO(Notification notification)
        {
            return new DataResponseNotification
            {
                Content = notification.Content,
                Id = notification.Id,
                IsSeen = notification.IsSeen,
                Link = notification.Link,
            };
        }
    }
}
