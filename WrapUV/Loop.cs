using WrapUV.Native;
using System;

namespace WrapUV
{
    public class Loop
    {
        private readonly LoopContext _loopContext;

        public Loop()
        {
            _loopContext = new LoopContext();
        }

        public Timer NewTimer(Action<object> callback, object state) =>
            new Timer(_loopContext.LoopPointer, callback, state);

        public Timer NewTimer(Action callback) =>
            NewTimer(ignore => callback(), null);

        public void RunDefault() =>
            _loopContext.Run();
    }
}
