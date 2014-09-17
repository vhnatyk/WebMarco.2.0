using WebMarco.Utilities.Logging;
using System;
using System.Collections.Generic;


namespace WebMarco.Utilities.Library {
    public class StopWatchTimer {
        DateTime start;
        public string Log;
        private string currentDescription;

        public void Start() {
            StartTheInterval();
            currentDescription = string.Empty;
        }

        public void Start(string description) {
            StartTheInterval();
            currentDescription = description;
        }

        public void StartNewLap(string description) {
            Stop();
            Start(description);
        }

        private void StartTheInterval() {
            start = DateTime.Now;
            //isPaused = false;			
        }

        //public void Pause()
        //{
        //    isPaused = true;
        //}

        public void Stop() {
            TimeSpan time = new TimeSpan(DateTime.Now.Ticks - start.Ticks);
            Log += string.Format("timer stop {0:000}m:{1:000}s:{2:000}ms {3}\n",
                                            time.Minutes,
                                                 time.Seconds,
                                                      time.Milliseconds,
                                                      string.IsNullOrWhiteSpace(currentDescription) ?
                                                      string.Empty : " - " + currentDescription);
        }

        public void Reset() {
            Log = string.Empty;
            //ticksElapsed = 0;
        }

        public override string ToString() {
            return Log;
        }

        public void WriteToLog() {
            DLogger.WriteLog(Log);
        }
    }
}
