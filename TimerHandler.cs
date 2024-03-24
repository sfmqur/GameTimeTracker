using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Win32;

namespace GameTimeTracker
{
    public class TimerHandler
    {
        public TimerHandler()
        {
            GameTimer = new System.Timers.Timer(1000);
            CurrentTime = new TimeSpan(0, 0, 0);
            GameTimer.Elapsed += OnTimerElapsed;
        }

        public TimeSpan CurrentTime { get; set; }
        public System.Timers.Timer GameTimer { get; }

        public void StartTimer()
        {
            GameTimer.Start();
        }

        public void StopTimer()
        {
            GameTimer.Stop();
        }

        public event EventHandler<TimeUpdatedEventArgs>? TimerElapsed;

        private void OnTimerElapsed(object? sender, EventArgs e)
        {
            CurrentTime += new TimeSpan(0, 1, 0);
            if (TimerElapsed != null) 
                TimerElapsed(this, new TimeUpdatedEventArgs(CurrentTime));

        }
    }
}