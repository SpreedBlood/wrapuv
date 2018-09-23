using System;
using WrapUV;

namespace TimerTest
{
    class Program
    {
        private readonly Loop _loop;
        private readonly Timer _timer;

        private Program()
        {
            _loop = new Loop();
            _timer = _loop.NewTimer(
                (number) =>
                {
                    Console.WriteLine(number);
                }, 5);
        }

        private void Run()
        {
            _timer.Start(0, 500);
            _loop.RunDefault();
        }

        static void Main() => new Program().Run();
    }
}
