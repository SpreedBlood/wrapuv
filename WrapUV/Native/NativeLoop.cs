using System;
using System.Runtime.InteropServices;

namespace WrapUV.Native
{
    internal static partial class Native
    {
        /// <summary>
        /// Gets the loop size memory, equivalent in C: sizeof(uv_loop_t)
        /// </summary>
        /// <returns>Returns the size of the loop.</returns>
        [DllImport("libuv", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr uv_loop_size();

        /// <summary>
        /// Tries to initialize the loop.
        /// </summary>
        /// <param name="handle">The loop pointer.</param>
        /// <returns>The result of the initialization.</returns>
        [DllImport("libuv", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int uv_loop_init(IntPtr handle);
    }
}
