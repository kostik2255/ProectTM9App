using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProectTM9App.Classes
{
    public class Date
    {
        public static string GetCurrentTime()
        {
            // присваивание текущего времени
            DateTime currentTime = DateTime.Now;
            // вывод
            return currentTime.ToString("HH:mm:ss, dd.MM.yyyy (dddd)");
        }
    }
}
