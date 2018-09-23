using System;

namespace WrapUV.Native.Contexts
{
    internal sealed unsafe class TimerContext : HandleContext
    {
        private readonly Action<object> _callback;
        private readonly object _state;

        internal TimerContext(
            IntPtr loopPtr,
            Action<object> callback,
            object state)
            : base(loopPtr, Uv_handle_type.UV_TIMER, Native.uv_timer_init)
        {
            _callback = callback;
            _state = state;
        }

        internal void Start(long timeout, long repeat)
        {
            IsAllocated();
            int result = Native.uv_timer_start(HandlePointer, WorkCallback, timeout, repeat);
            Native.CheckIfError(result);
        }

        internal void Stop()
        {
            IsAllocated();
            int result = Native.uv_timer_stop(HandlePointer);
            Native.CheckIfError(result);

            Close();
        }

        internal void SetRepeat(long repeat)
        {
            IsAllocated();
            Native.uv_timer_set_repeat(HandlePointer, repeat);
        }

        internal long GetRepeat()
        {
            IsAllocated();
            return Native.uv_timer_get_repeat(HandlePointer);
        }

        private void OnWorkCallback()
        {
            _callback(_state);
        }

        private static void WorkCallback(IntPtr handle)
        {
            TimerContext timer = FromPointer<TimerContext>(handle);
            timer.OnWorkCallback();
        }
    }
}