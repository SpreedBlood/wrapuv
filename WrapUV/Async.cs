using System;
using WrapUV.Native.Contexts;

namespace WrapUV
{
    public class Async
    {
        public object Data { get; set; }

        private readonly AsyncContext _asyncContext;

        internal Async(
            IntPtr loopHandle,
            Action<Async> callback)
        {
            _asyncContext = new AsyncContext(loopHandle, () => callback(this));
        }

        public void Send() =>
            _asyncContext.Send();

        public void Stop() =>
            _asyncContext.Close();
    }
}