using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WrapUV.Native
{
    internal static partial class Native
    {
        /// <summary>
        /// Checks if the result is an error. If the result is less than 0 then
        /// it's an error.
        /// </summary>
        /// <param name="code">The result (error).</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void CheckIfError(int code)
        {
            if (code < 0)
            {
                throw CreateError((Uv_err_code)code);
            }
        }

        /// <summary>
        /// Createsz the error exception with the name and description.
        /// </summary>
        /// <param name="error">The error code.</param>
        /// <returns>The built exception with details of the error code.</returns>
        private static Exception CreateError(Uv_err_code error)
        {
            IntPtr ptr = uv_err_name(error);
            string name = ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(ptr) : null;

            ptr = uv_strerror(error);
            string description = ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(ptr) : null;

            return new Exception($"Error: {error}, name: {name}, description: {description}");
        }

        /// <summary>
        /// Gets the name of the error code.
        /// </summary>
        /// <param name="err">The error code.</param>
        /// <returns>The pointer to the error name.</returns>
        [DllImport("libuv", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr uv_err_name(Uv_err_code err);

        /// <summary>
        /// The description of the error code.
        /// </summary>
        /// <param name="err">The error code.</param>
        /// <returns>The pointer to the description.</returns>
        [DllImport("libuv", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr uv_strerror(Uv_err_code err);
    }
}
