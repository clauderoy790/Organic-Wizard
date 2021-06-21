using System.Timers;

namespace Shared
{
    public static  class TimerExt
    {
        public static void Restart(this Timer timer)
        {
            timer.Stop();
            timer.Start();
        }
    }
}
