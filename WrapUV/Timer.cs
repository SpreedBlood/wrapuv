using WrapUV.Native;
using System;

namespace WrapUV
{
    public class Timer
    {
        private readonly TimerContext _timerContext;

        internal Timer(
            IntPtr loopHandle,
            Action<object> callback,
            object state)
        {
            _timerContext = new TimerContext(loopHandle, callback, state);
        }

        public void Start(long timeout, long repeat) =>
            _timerContext.Start(timeout, repeat);

        public void Stop() =>
            _timerContext.Stop();

        public void SetRepeat(long repeat) =>
            _timerContext.SetRepeat(repeat);

        public long GetRepeat() =>
            _timerContext.GetRepeat();
    }
}
