using System;
using System.Threading;

namespace SpinLockDemo
{
    /// <summary>
    /// SpinLock
    /// 封装了SpinWaite的逻辑，
    /// </summary>
    public class ThreadSpinLockDemo
    {
        private static SpinLock _spinLock = new SpinLock();
        private static int _counterA = 0;
        private static int _counterB = 0;

        public static void IncrementCounters()
        {
            bool lockTaken = false;
            try
            {
                _spinLock.Enter(ref lockTaken);
                ++_counterA;
                ++_counterB;
            }
            finally
            {
                if (lockTaken)
                {
                    _spinLock.Exit();
                }
            }
        }

        public static void GetCounters(out int counterA, out int counterB)
        {
            bool lockTaken = false;
            try
            {
                _spinLock.Enter(ref lockTaken);
                counterA = _counterA;
                counterB = _counterB;
            }
            finally
            {
                if (lockTaken)
                {
                    _spinLock.Exit();
                }
            }
        }
    }
}