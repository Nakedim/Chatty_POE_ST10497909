using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CyberChat.Views.LogActivities;

namespace CyberChat.Views
{
    public class MainAppView
    {
        public ObservableCollection<ActivityLogItem> Activities { get; set; }


        public MainAppView()
        {
            // Constructor logic here
        }
        public void logActivities(string description)
        {
            Activities.Add(new ActivityLogItem
            {
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Action = "Action Description", 
                Details = description
            });
        }
    }
}
