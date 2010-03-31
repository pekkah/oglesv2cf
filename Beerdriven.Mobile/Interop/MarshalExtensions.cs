namespace Beerdriven.Mobile.Interop
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MarshalExtensions
    {
        public enum CodePage
        {
            CP_ACP = 0, // default to ANSI code page
            CP_OEMCP = 1, // default to OEM  code page
            CP_MACCP = 2, // default to MAC  code page
            CP_THREAD_ACP = 3, // current thread's ANSI code page
            CP_SYMBOL = 42, // SYMBOL translations

            CP_UTF7 = 65000, // UTF-7 translation
            CP_UTF8 = 65001, // UTF-8 translation
        }

        public static string PtrToStingAnsi(IntPtr ptr)
        {
            int bufferSize = MultiByteToWideChar(CodePage.CP_ACP, 0, ptr, -1, null, 0);

            if (0 == bufferSize)
            {
                throw new ArgumentException(
                        String.Format(
                                "Unable to get ANSI string for given pointer. Error: {0}", Marshal.GetLastWin32Error()));
            }

            var buffer = new StringBuilder(bufferSize);

            bufferSize = MultiByteToWideChar(CodePage.CP_ACP, 0, ptr, -1, buffer, buffer.Capacity);

            if (0 == bufferSize)
            {
                throw new ArgumentException(
                        String.Format(
                                "Unable to convert ANSI string to Unicode string for given pointer. Error: {0}",
                                Marshal.GetLastWin32Error()));
            }

            return buffer.ToString();
        }

        public static IntPtr StringToPtrAnsi(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return IntPtr.Zero;
            }

            byte[] bytes = Encoding.ASCII.GetBytes(str + '\0');
            IntPtr strPtr = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, strPtr, bytes.Length);

            return strPtr;
        }

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern int MultiByteToWideChar(
                CodePage CodePage,
                int dwFlags,
                IntPtr lpMultiByteStr,
                int cbMultiByte,
                StringBuilder lpWideCharStr,
                int cchWideChar);
    }
}