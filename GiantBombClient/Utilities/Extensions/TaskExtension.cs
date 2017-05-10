using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GiantBombClient.Utilities.Extensions
{
    public static class TaskExtension
    {
        public static void LogAndContinueOnFaulted(this Task task)
        {
            task.ContinueWith(t =>
            {

                if (t.IsFaulted)
                {
                    Debug.WriteLine("[DEBUG] Save Video Time Failed");
                }

            });
        }
    }
}
