using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProectTM9App.Classes
{
    public class RequestTracker
    {
        private readonly List<DateTime> requestTimes = new List<DateTime>();
        private readonly int maxRequests = 3;
        private readonly TimeSpan timeWindow = TimeSpan.FromMinutes(30);

        public bool CanSendRequest()
        {
            // Удаление старых заявок
            requestTimes.RemoveAll(time => DateTime.Now - time > timeWindow);

            // Проверка. можно ли мы отправить новую заявку
            if (requestTimes.Count < maxRequests)
            {
                requestTimes.Add(DateTime.Now);
                return true;
            }

            return false;
        }
    }
}
