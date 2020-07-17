using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shared
{
    public static class DebugUtils
    {
        public static int CheckExecutionTime(Action action)
        {
            Stopwatch watch = Stopwatch.StartNew();
            action?.Invoke();
            watch.Stop();
            return (int)Math.Round(watch.ElapsedMilliseconds / 1000f);
        }
    }
}
