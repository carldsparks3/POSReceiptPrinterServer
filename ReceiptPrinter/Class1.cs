﻿using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace ReceiptPrinter
{
    public static class Class1
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, FileMode dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        public static bool Print()
        {
            string nl = Convert.ToChar(13).ToString() + Convert.ToChar(10).ToString();
            bool IsConnected = false;

            string sampleText = "Hello World!" + nl +
            "Enjoy Printing...";
            try
            {
                Byte[] buffer = new byte[sampleText.Length];
                buffer = System.Text.Encoding.ASCII.GetBytes(sampleText);

                SafeFileHandle fh = CreateFile("LPT1:", FileAccess.Write, 0, IntPtr.Zero, FileMode.OpenOrCreate, 0, IntPtr.Zero);
                if (!fh.IsInvalid)
                {
                    IsConnected = true;
                    FileStream lpt1 = new FileStream(fh, FileAccess.ReadWrite);
                    lpt1.Write(buffer, 0, buffer.Length);
                    lpt1.Close();
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return IsConnected;
        }
    }
}
