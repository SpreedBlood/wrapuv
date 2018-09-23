using System;
using System.Runtime.InteropServices;

namespace WrapUV.Native.Contexts
{
    internal sealed unsafe class LoopContext
    {
        internal IntPtr LoopPointer { get; }

        internal LoopContext()
        {
            IntPtr loopPtr = Marshal.AllocCoTaskMem(Native.uv_loop_size().ToInt32());

            try
            {
                int result = Native.uv_loop_init(loopPtr);
                Native.CheckIfError(result);
            }
            catch
            {
                Marshal.FreeCoTaskMem(loopPtr);
                throw;
            }

            GCHandle gcHandle = GCHandle.Alloc(this);
            ((Uv_loop_t*)loopPtr)->data = GCHandle.ToIntPtr(gcHandle);
            LoopPointer = loopPtr;
        }

        /// <summary>
        /// Runs the loop with the given mode. If no parameter is given, it will
        /// run in default mode.
        /// </summary>
        /// <param name="mode">The mode to run the loop in.</param>
        internal void Run(Uv_run_mode mode = Uv_run_mode.UV_RUN_DEFAULT)
        {
            IsAllocated();
            Native.uv_run(LoopPointer, mode);
        }

        /// <summary>
        /// Checks if the loop is allocated, if it isn't then throw exception.
        /// </summary>
        private void IsAllocated()
        {
            if (LoopPointer == IntPtr.Zero)
            {
                throw new InvalidOperationException("The loop isn't allocated!");
            }
        }
    }
}
