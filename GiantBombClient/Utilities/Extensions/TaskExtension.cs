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
        public static async void LogAndContinueOnFaulted(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception)
            {
                Debug.WriteLine("[DEBUG] Task failed, continuing");
            }
        }
    }
}
