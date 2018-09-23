using System;
using System.Runtime.InteropServices;

namespace WrapUV.Native
{
    internal static partial class Native
    {
        /// <summary>
        /// Runs the handle with the given mode.
        /// </summary>
        /// <param name="handle">The pointer to the handle.</param>
        /// <param name="mode">The mode to run in.</param>
        /// <returns>The result of the run operation.</returns>
        [DllImport("libuv", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int uv_run(IntPtr handle, Uv_run_mode mode);

        /// <summary>
        /// Gets the size of the handle (Equivalent in C: sizeof(handle_type);)
        /// </summary>
        /// <param name="handleType">The handle type to get the size of.</param>
        /// <returns>The pointer to the size.</returns>
        [DllImport("libuv", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr uv_handle_size(Uv_handle_type handleType);

        /// <summary>
        /// Start the timer. (Timeout & repeat are in milliseconds).
        /// </summary>
        /// <param name="handle">The pointer to the handle.</param>
        /// <param name="timer_cb">The timer callback that's being invoked.</param>
        /// <param name="timeout">Amount of milliseconds to wait before starting.</param>
        /// <param name="repeat">How many millseconds it should wait between cycles.</param>
        /// <returns></returns>
        [DllImport("libuv", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int uv_timer_start(IntPtr handle, uv_timer_cb timer_cb, long timeout, long repeat);

        /// <summary>
        /// Sets the delay between cycles.
        /// </summary>
        /// <param name="handle">The pointer to the handle.</param>
        /// <param name="repeat">The milliseconds between each cycle.</param>
        [DllImport("libuv", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void uv_timer_set_repeat(IntPtr handle, long repeat);

        /// <summary>
        /// Gets the repeat in millseconds that's set to the timer.
        /// </summary>
        /// <param name="handle">The pointer th the timer handle.</param>
        /// <returns>The repeat in millseconds.</returns>
        [DllImport("libuv", CallingConvention = CallingConvention.Cdecl)]
        internal static extern long uv_timer_get_repeat(IntPtr handle);

        /// <summary>
        /// Attempts to stop the timer.
        /// </summary>
        /// <param name="handle">The pointer to the timer handle.</param>
        /// <returns>The result of the attempt.</returns>
        [DllImport("libuv", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int uv_timer_stop(IntPtr handle);
    }
}
