using System;
using System.Runtime.InteropServices;

namespace WrapUV.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Uv_loop_t
    {
        /// <summary>
        /// The data of the loop.
        /// </summary>
        public IntPtr data;

        /// <summary>
        /// The active handles that the loop holds.
        /// </summary>
        public uint active_handles;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct Uv_handle_t
    {
        /// <summary>
        /// The data of the handle.
        /// </summary>
        public IntPtr data;

        /// <summary>
        /// The pointer to the loop handle.
        /// </summary>
        public IntPtr loop;

        /// <summary>
        /// The type of the handle.
        /// </summary>
        public Uv_handle_type type;

        /// <summary>
        /// The pointer to the close callback that's invoked when the handle closes.
        /// </summary>
        public IntPtr close_cb;
    }
}
