using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Colossal {
    public class CustomConsole : MonoBehaviour {
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        private IntPtr consoleWindowHandle;
        private StreamWriter logWriter;

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeConsole();

        private void Awake() {
            AllocConsole();
            consoleWindowHandle = GetConsoleWindow();
            ShowWindow(consoleWindowHandle, SW_SHOW);
            logWriter = new StreamWriter(Console.OpenStandardOutput());
            logWriter.AutoFlush = true;
            Console.SetOut(logWriter);

            CustomConsole.LogToConsole(
                @"
                _________  ________  .____    ________    _________ _________   _____  .____     
                \_   ___ \ \_____  \ |    |   \_____  \  /   _____//   _____/  /  _  \ |    |    
                /    \  \/  /   |   \|    |    /   |   \ \_____  \ \_____  \  /  /_\  \|    |    
                \     \____/    |    \    |___/    |    \/        \/        \/    |    \    |___ 
                 \______  /\_______  /_______ \_______  /_______  /_______  /\____|__  /_______ \
                        \/         \/        \/       \/        \/        \/         \/        \/
                                Trolling lemming harder since July, 19th. 2022
                ");
        }

        private void OnApplicationQuit() {
            FreeConsole();
            logWriter.Close();
        }

        public static void LogToConsole(string message) {
            Console.WriteLine(message);
        }
    }
}