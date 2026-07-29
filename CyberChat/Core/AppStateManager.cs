using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChat.Core
{
    public class AppStateManager
    {
        public static List<string> ActivityLog = new List<string>();

        public static void TrackAction(string action)
        {
            ActivityLog.Add($"{DateTime.Now}: {action}");
        }
        private List<string> FetchLogFromSource()
        {
            return CyberChat.Core.AppStateManager.ActivityLog;

        }
    }
}