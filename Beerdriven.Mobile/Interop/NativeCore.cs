#region license

// Copyright (c) 2010 Pekka Heikura
// 
//  Permission is hereby granted, free of charge, to any person
//  obtaining a copy of this software and associated documentation
//  files (the "Software"), to deal in the Software without
//  restriction, including without limitation the rights to use,
//  copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following
//  conditions:
// 
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//  OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//  OTHER DEALINGS IN THE SOFTWARE.
// 

#endregion

namespace Beerdriven.Mobile.Interop
{
    using System;
    using System.Runtime.InteropServices;

    public static class NativeCore
    {
        public const string CoreDllName = "coredll";

        [DllImport(CoreDllName)]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport(CoreDllName)]
        public static extern IntPtr LocalAlloc(uint flags, uint cb);

        [DllImport(CoreDllName)]
        public static extern IntPtr LocalFree(IntPtr p);

        [DllImport(CoreDllName, CharSet = CharSet.Auto)]
        public static extern bool PeekMessage(
                out NativeMessage lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport(CoreDllName)]
        public static extern bool QueryPerformanceCounter(ref Int64 lpPerformanceCount);

        [DllImport(CoreDllName)]
        public static extern bool QueryPerformanceFrequency(ref Int64 lpFrequency);
    }
}