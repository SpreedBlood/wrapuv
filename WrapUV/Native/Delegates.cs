using System;
using System.Runtime.InteropServices;

namespace WrapUV.Native
{
    /// <summary>
    /// Work delegate that's invoked upon handle working.
    /// </summary>
    /// <param name="handle">The pointer to the handle that's working.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void uv_work_cb(IntPtr handle);
   
    /// <summary>
    /// The callback that's invoked when the timer proceeds.
    /// </summary>
    /// <param name="handle">The pointer to the timer handle.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void uv_timer_cb(IntPtr handle);

    /// <summary>
    /// The callback that's invoked when closing a handle.
    /// </summary>
    /// <param name="handle">The pointer to the handle that's closing.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void uv_close_cb(IntPtr handle);

    /// <summary>
    /// The callback that's invoked when async operation returns.
    /// </summary>
    /// <param name="handle">The pointer to the async handle.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void uv_async_cb(IntPtr handle);
}