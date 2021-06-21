using System;
using System.Threading;

namespace MixedLockDemo
{
    /// <summary>
    /// 混合锁Demo
    /// </summary>
    public static class NitiveLockDemo
    {
        private static readonly object _lock = new();
        private static int _counterA = 0;
        private static int _counterB = 0;

        /// <summary>
        /// 增加
        /// </summary>
        public static void IncrementCounters()
        {
            //定义变量
            var lockObject = _lock;
            bool lockTaken = false;
            try
            {
                Console.WriteLine($"开始执行锁前的数值:{_counterA},{_counterB}");
                // 获取锁
                Monitor.Enter(_lock, ref lockTaken);
                ++_counterA;
                ++_counterB;

            }
            finally
            {
                //如果锁wei
                if (!lockTaken)
                {
                    Monitor.Exit(lockObject);
                }
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="counterA"></param>
        /// <param name="coubterB"></param>
        public static void GetCounters(ref int counterA, ref int coubterB)
        {
            //定义变量
            var lockObject = _lock;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(lockObject, ref lockTaken);
                counterA = _counterA;
                coubterB = _counterB;
            }
            finally
            {
                if (!lockTaken)
                {
                    Monitor.Exit(lockObject);
                }
            }
        }
    }
}