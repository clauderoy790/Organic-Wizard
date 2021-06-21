using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Shared
{
    public class CTimer
    {
        private Timer _timer;
        private Stopwatch _watch;


        public event ElapsedEventHandler Elapsed
        {
            add 
            {
                _timer.Elapsed -= value;
                _timer.Elapsed += value; 
            }
            remove { _timer.Elapsed -= value; }
        }

        public bool Enabled
        {
            get { return _timer.Enabled; }
            set { _timer.Enabled = value; }
        }

        public double Interval
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        public double IntervalSeconds
        {
            get { return _timer.Interval/1000; }
            set { _timer.Interval = value*1000; }
        }

        public bool AutoReset
        {
            get { return _timer.AutoReset; }
            set { _timer.AutoReset = value; }
        }

        public Timer Source
        {
            get { return _timer; }
        }

        public double ElapsedMs { get { return _watch.ElapsedMilliseconds; } }
        public double ElapsedSeconds { get { return ElapsedMs/1000; } }

        public CTimer(double interval,ElapsedEventHandler elapsed)
        {
            Init(interval,elapsed);
        }

        public CTimer(double interval)
        {
            Init(interval,null);
        }

        public CTimer()
        {
            Init(0,null);
        }

        private void Init(double interval, ElapsedEventHandler elapsed)
        {
            _timer = new Timer();
            _timer.AutoReset = false;
            _timer.Elapsed += OnElapsed;
            _watch = new Stopwatch();

            if (interval > 0)
                _timer.Interval = interval;
            else
                _timer.Interval = 1;

            if (elapsed != null)
                _timer.Elapsed += elapsed;
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            if (!_timer.Enabled)
                _watch.Stop();
        }

        public void Start()
        {
            if (!_timer.Enabled)
                _watch.Reset();
                
            _watch.Start();
            _timer.Start();
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _watch.Stop();
        }
    }
}
