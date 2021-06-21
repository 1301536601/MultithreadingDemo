using System;
using System.Threading;

namespace TheMutexDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MutexDemo1.IncrementCounters();
            Console.WriteLine("Hello World!");
        }
    }

    public static class MutexDemo1
    {
        private static Mutex _lock = new Mutex(false, @"Walterlv.Mutex");
        private static int _counterA = 0;
        private static int _counterB = 0;

        public static void IncrementCounters()
        {
            //获取锁
            _lock.WaitOne();
            try
            {
                ++_counterA;
                ++_counterB;
            }
            finally
            {
                //释放锁
                _lock.ReleaseMutex();
            }
        }

        public static void GetCounters(out int counterA, out int counterB)
        {
            _lock.WaitOne();
            try
            {
                counterA = _counterA;
                counterB = _counterB;
            }
            finally
            {
                //释放锁
                _lock.ReleaseMutex();
            }
        }
    }

}
