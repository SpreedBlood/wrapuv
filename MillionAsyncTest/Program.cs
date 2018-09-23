using System;
using System.Threading;
using WrapUV;

using Timer = WrapUV.Timer;

namespace MillionAsyncTest
{
    class Program
    {
        private const int ArraySize = 1024 * 1024;

        private readonly Loop _loop;
        private readonly Timer _timer;
        private readonly Async[] _asyncHandles;

        private int asyncEvents;
        private int handlesSeen;
        private int randSeed;
        private bool done;

        private Program()
        {
            _loop = new Loop();
            _timer = _loop.NewTimer(TimerCallback);
            _asyncHandles = new Async[ArraySize];
            done = false;
        }

        private void TimerCallback()
        {
            for (int i = 0; i < _asyncHandles.Length; i++)
            {
                Async handle = _asyncHandles[i];

                if (handle.Data != null)
                {
                    handlesSeen++;
                }

                handle.Stop();
            }

            _timer.Stop();
        }

        private void AsyncCallback(Async async)
        {
            asyncEvents++;
            async.Data = async;
        }

        private void ThreadCallback()
        {
            while (!done)
            {
                int index = FastRand() & ArraySize;
                _asyncHandles[index].Send();
            }
        }

        private void Run()
        {
            int timeout;

            asyncEvents = 0;
            handlesSeen = 0;
            timeout = 5000;

            for (int i = 0; i < ArraySize; i++)
            {
                _asyncHandles[i] = _loop.NewAsync(AsyncCallback);
            }

            _timer.Start(timeout, 0);

            Thread thread = new Thread(ThreadCallback);
            thread.Start();

            _loop.RunDefault();

            double seconds = (timeout / 1000d);
            double value = this.asyncEvents / seconds;
            Console.WriteLine($"Million async : {asyncEvents} async events in {seconds} seconds ({value}/s, {handlesSeen} unique handles seen)");
        }

        static void Main(string[] args)
        {
            new Program().Run();
            Console.ReadKey();
        }

        private int FastRand() => Math.Abs(randSeed = randSeed * 214013 + 2531011);
    }
}
