using System;
using System.Threading;

namespace SpinLockDemo
{
    /// <summary>
    /// 通过Thread.SpinWait实现自旋锁
    /// 基于Test--And--Set原子操作实现
    /// 使用一个数据表示当前锁是否已经被获取 0表示未被索取，1表示已经获取 获取锁时会将_lock的值设置为1 然后检查修改前的值是否等于0，
    /// 优点：
    /// 1、不使用Thread.SpinWait方法,重试的方法体会为空，CPU会使用它的最大性能来不断的进行赋值和比较指令，会浪费很大的性能
    /// Thread.SpinWait提示CPU当前正在自旋锁的循环中，可以休息若干个时间周期
    /// 1：使用自旋锁需要注意的问题，自旋锁保护的代码应该在非常短的时间内执行完成，如果时间过长，其他线程不断重试导致影响其他线程进行
    /// 缺点：
    /// 当前实现没有考虑到公平性，如果多个线程同时获取锁失败，按时间顺序第一个获取锁的线程不一定会在释放锁后第一个获取成功，
    /// </summary>
    public static class ThreadSpinWaitDemo
    {
        private static int _lock = 0;
        private static int _counterA = 0;
        private static int _counterB = 0;

        public static void IncrementCounters()
        {
            while (Interlocked.Exchange(ref _lock, 1) != 0)
            {
                Thread.SpinWait(1);
            }

            ++_counterA;
            ++_counterB;
            Interlocked.Exchange(ref _lock, 0);
        }

        public static void GetCounters(out int counterA, out int counterB)
        {
            while (Interlocked.Exchange(ref _lock, 1) != 0)
            {
                Thread.SpinWait(1);
            }
            counterA = _counterA;
            counterB = _counterB;
            Interlocked.Exchange(ref _lock, 0);

        }
    }
}