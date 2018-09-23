using System;
using System.Runtime.InteropServices;

namespace WrapUV.Native.Contexts
{
    internal unsafe class HandleContext : IDisposable
    {
        internal IntPtr HandlePointer { get; private set; }

        internal HandleContext(
            IntPtr loopHandle,
            Uv_handle_type handleType,
            Func<IntPtr, IntPtr, int> handleInit)
        {
            int handleSize = Native.uv_handle_size(handleType).ToInt32();
            IntPtr handlePointer = Marshal.AllocCoTaskMem(handleSize);

            try
            {
                int result = handleInit(loopHandle, handlePointer);
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
        /// Closes the handle & releases all the resources.
        /// </summary>
        public void Dispose()
        {
            IntPtr handlePointer = ((Uv_handle_t*)HandlePointer)->data;
            //Make sure that the handle pointer is valid.
            if (handlePointer != IntPtr.Zero)
            {
                GCHandle handle = GCHandle.FromIntPtr(handlePointer);
                if (handle.IsAllocated)
                {
                    //Free the gc handle.
                    handle.Free();

                    //Make the data of the handle invalid.
                    ((Uv_handle_t*)HandlePointer)->data = IntPtr.Zero;
                }
            }

            //Free the resources & make the pointer invalid.
            Marshal.FreeCoTaskMem(HandlePointer);
            HandlePointer = IntPtr.Zero;
        }

        /// <summary>
        /// Starts closing the handle.
        /// </summary>
        internal void Close()
        {
            int result = Native.uv_is_closing(HandlePointer);

            //Make sure that the handle isn't closing.
            if (result == 0)
            {
                Native.uv_close(HandlePointer, CloseCallback);
            }
        }

        private static void CloseCallback(IntPtr handle)
        {
            IDisposable disposableTarget = FromPointer<IDisposable>(handle);
            disposableTarget.Dispose();
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
