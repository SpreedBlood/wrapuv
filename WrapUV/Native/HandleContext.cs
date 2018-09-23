using System;
using System.Runtime.InteropServices;

namespace WrapUV.Native
{
    internal unsafe class HandleContext
    {
        internal IntPtr HandlePointer { get; }

        internal HandleContext(
            IntPtr loopHandle,
            Uv_handle_type handleType)
        {
            int handleSize = Native.uv_handle_size(handleType).ToInt32();
            IntPtr handlePointer = Marshal.AllocCoTaskMem(handleSize);

            try
            {
                int result = Native.uv_timer_init(loopHandle, handlePointer);
                Native.CheckIfError(result);
            }
            catch
            {
                Marshal.FreeCoTaskMem(handlePointer);
                throw;
            }

            GCHandle gcHandle = GCHandle.Alloc(this);
            ((Uv_handle_t*)handlePointer)->data = GCHandle.ToIntPtr(gcHandle);

            HandlePointer = handlePointer;
        }

        /// <summary>
        /// Checks if the handle is allocated, if it isn't then throw exception.
        /// </summary>
        protected void IsAllocated()
        {
            if (HandlePointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"The handle isn't allocated! {GetType()}");
            }
        }

        /// <summary>
        /// Tries to get the pointer from the <see cref="GCHandle"/>.
        /// </summary>
        /// <typeparam name="T">The type to parse into.</typeparam>
        /// <param name="handle">The pointer to the handle.</param>
        /// <returns>The handle.</returns>
        protected static T FromPointer<T>(IntPtr handle)
        {
            if (handle != IntPtr.Zero) //Check if the handle is allocated.
            {
                //Get the pointer to the gc && make sure that it's allocated.
                IntPtr gcHandlePtr = ((Uv_handle_t*)handle)->data;
                if (gcHandlePtr != IntPtr.Zero)
                {
                    GCHandle gcHandle = GCHandle.FromIntPtr(gcHandlePtr);
                    if (gcHandle.IsAllocated)
                    {
                        return (T)gcHandle.Target;
                    }
                }
            }

            return default(T);
        }
    }
}
