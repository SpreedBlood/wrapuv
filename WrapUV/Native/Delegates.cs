using System;
using System.Runtime.InteropServices;

namespace WrapUV.Native
{
    /// <summary>
    /// Work delegate that's invoked upon handle working.
    /// </summary>
    /// <param name="handle">The pointer to the handle that's working.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void uv_work_cb(IntPtr handle);
   
    /// <summary>
    /// The callback that's invoked when the timer proceeds.
    /// </summary>
    /// <param name="handle">The pointer to the timer handle.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void uv_timer_cb(IntPtr handle);
}