using System;
using System.Runtime.InteropServices;

namespace WrapUV.Native
{
    internal static partial class Native
    {
        /// <summary>
        /// Initializes the async handle.
        /// </summary>
        /// <param name="loop">The pointer to the loop handle.</param>
        /// <param name="async">The pointer to the async handle.</param>
        /// <param name="async_cb">The async callback.</param>
        /// <returns>The initialization result.</returns>
        [DllImport("libuv", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int uv_async_init(IntPtr loop, IntPtr async, uv_async_cb async_cb);

        /// <summary>
        /// Wakes the event loop up & calls the async callback.
        /// </summary>
        /// <param name="async">The pointer to the async handle.</param>
        /// <returns>The result of the call.</returns>
        [DllImport("libuv", CallingConvention =CallingConvention.Cdecl)]
        internal static extern int uv_async_send(IntPtr async);
    }
}
