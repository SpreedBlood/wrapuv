using System;
using System.Runtime.InteropServices;

namespace WrapUV.Native
{
    internal static partial class Native
    {
        /// <summary>
        /// Initializes a timer.
        /// </summary>
        /// <param name="loopHandle">The pointer to the loop handle.</param>
        /// <param name="handle">The pointer to the allocated timer handle.</param>
        /// <returns>The result of the timer initialization.</returns>
        [DllImport("libuv", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int uv_timer_init(IntPtr loopHandle, IntPtr handle);
    }
}
