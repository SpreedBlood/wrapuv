using System;

namespace WrapUV.Native.Contexts
{
    internal class AsyncContext : HandleContext
    {
        private readonly Action _callback;

        internal AsyncContext(
            IntPtr loopHandle,
            Action callback)
            : base(loopHandle, Uv_handle_type.UV_ASYNC, InitializeAsyncHandle)
        {
            _callback = callback;
        }

        internal void Send()
        {
            int result = Native.uv_async_send(HandlePointer);
            Native.CheckIfError(result);
        }

        private void OnAsyncCallback()
        {
            _callback();
        }

        private static void AsyncCallback(IntPtr handle)
        {
            AsyncContext async = FromPointer<AsyncContext>(handle);
            async.OnAsyncCallback();
        }

        private static int InitializeAsyncHandle(IntPtr loop, IntPtr asyncHandle) =>
            Native.uv_async_init(loop, asyncHandle, AsyncCallback);
    }
}
