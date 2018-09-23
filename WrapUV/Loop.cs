using System;
using WrapUV.Native.Contexts;

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

        public Async NewAsync(Action<Async> callback) =>
            new Async(_loopContext.LoopPointer, callback);

        public void RunDefault() =>
            _loopContext.Run();
    }
}
