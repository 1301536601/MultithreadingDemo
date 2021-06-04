using System;
using System.Threading;

namespace MultiThreadingDemo.Demo
{
    public static class ThreadStaticDemo
    {
        [ThreadStatic]
        public static int ThreadStaticNum = 123;

        public static void Show()
        {
            Console.WriteLine($@"Current Is New Thread ,ThreadId Is {Thread.CurrentThread.ManagedThreadId} Current ThreadNum Is {ThreadStaticNum}");
        }
    }
}