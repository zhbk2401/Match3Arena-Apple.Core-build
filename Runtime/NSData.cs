using System;
using System.Runtime.InteropServices;

namespace Apple.Core.Runtime
{
    /// <summary>
    /// C# wrapper around NSData.
    /// </summary>
    public class NSData : NSObject
    {
        /// <summary>
        /// Construct an NSData wrapper around an existing instance.
        /// </summary>
        /// <param name="pointer"></param>
        public NSData(IntPtr pointer) : base(pointer)
        {
        }

        /// <summary>
        /// Default constructor that initializes an empty NSData instance.
        /// </summary>
        public NSData() : base(CreateEmptyNSData())
        {
        }

        /// <summary>
        /// The number of bytes contained by the data object.
        /// </summary>
        public UInt64 Length => Interop.NSData_GetLength(Pointer, NSException.ThrowOnExceptionCallback);

        /// <summary>
        /// Return the object's contents as a byte array.
        /// </summary>
        public byte[] Bytes
        {
            get
            {
                byte[] bytes = null;

                var length = (int)Length;
                var bytePtr = Interop.NSData_GetBytes(Pointer, NSException.ThrowOnExceptionCallback);
                if (length >= 0 && bytePtr != IntPtr.Zero)
                {
                    bytes = new byte[length];
                    Marshal.Copy(bytePtr, bytes, 0, length);
                }

                return bytes;
            }
        }

        /// <summary>
        /// Creates an empty NSData instance using a native method.
        /// </summary>
        /// <returns>Pointer to the newly created empty NSData object.</returns>
        private static IntPtr CreateEmptyNSData()
        {
            // Replace this with the actual method to create an empty NSData object
            return Interop.NSData_CreateEmpty(NSException.ThrowOnExceptionCallback);
        }

        private static class Interop
        {
            [DllImport(InteropUtility.DLLName)] public static extern UInt64 NSData_GetLength(IntPtr nsDataPtr, NSExceptionCallback onException);
            [DllImport(InteropUtility.DLLName)] public static extern IntPtr NSData_GetBytes(IntPtr nsDataPtr, NSExceptionCallback onException);

            [DllImport(InteropUtility.DLLName)] public static extern IntPtr NSData_CreateEmpty(NSExceptionCallback onException);
        }
    }
}