using System;

namespace MixedLockDemo
{

    /// <summary>
    /// 启动类
    /// </summary>
    class Program
    {

        /// <summary>
        /// 启动函数
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            NitiveLockDemo.IncrementCounters();
            var numberA = 0;
            var numberB = 0;
            NitiveLockDemo.GetCounters(ref numberA, ref numberB);
            Console.WriteLine($"执行锁后得到的数值:{numberA},{numberB}");
            Console.ReadKey();
        }
    }
}
