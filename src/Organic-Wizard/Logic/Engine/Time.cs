using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Organic_Wizard
{
    public static class Time
    {
        static Stopwatch _watch = new Stopwatch();
        public static  float DeltaTime { get; private set; }


        public static void StartFrame()
        {
            _watch.Restart();
        }

        public static void EndFrame()
        {
            DeltaTime = _watch.ElapsedMilliseconds/1000f;
        }
    }
}
