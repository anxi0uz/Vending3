using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Models
{
    public class NotificationLog
    {
        public int Id { get; set; }
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }

    public enum NotificationType
    {
        Success,
        Warning,
        Error,
        Information
    }
}
