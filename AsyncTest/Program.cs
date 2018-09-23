using System;
using System.Threading.Tasks;
using WrapUV;

namespace AsyncTest
{
    class Program
    {
        private readonly Loop _loop;
        private readonly Async _async;

        private Program()
        {
            _loop = new Loop();
            _async = _loop.NewAsync((async) =>
            {
                Console.WriteLine("test");
            });
        }

        private void Run()
        {
            Task.Run(async delegate
            {
                await Task.Delay(1000);
                _async.Send();
            });
            _loop.RunDefault();
        }

        static void Main() => new Program().Run();
    }
}
