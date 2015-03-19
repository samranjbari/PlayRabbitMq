using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ.Console
{
    public class Watcher
    {
        private Stopwatch watch;

        public Watcher(bool autostart = false)
        {
            watch = new Stopwatch();
            if (autostart)
            {
                watch.Start();
            }
        }

        public void Start()
        {
            if (watch.IsRunning)
            {
                watch.Stop();
            }

            watch.Start();
        }

        public void Stop()
        {
            watch.Stop();
        }

        public void Display()
        {
            watch.Stop();

            System.Console.WriteLine("Finished in {0} seconds", watch.ElapsedMilliseconds / 1000);
        }
    }
}
