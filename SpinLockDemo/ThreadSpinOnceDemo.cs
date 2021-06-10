using System.Threading;

namespace SpinLockDemo
{
    /// <summary>
    /// SpinWaite实现自旋锁
    /// 特性是SpinOnce方法的次数，如果在一定次数以内并且当前逻辑核心所大于1，则调用Thread.SpinWait函数；如果超过一定次数或者当前环境逻辑核心数等于1，则交替使用
    /// Thread.Sleep(0)和Thread.Yield函数，表示切换到其他线程，如果再超过一定次数，则让当前线程休眠
    /// SpinWaite解决Thread.SpinWait中的两个问题
    /// 1：如果自旋锁运行时间超长，SpinWaite可以提示操作系统切换到其他线程或者让当前线程进入休眠状态，
    /// 2：如果当前环境只有一个核心逻辑，SpinWaite不会执行Thread.SpinWait函数，而是直接提示操作系统切换到其他线程，
    /// </summary>
    public static class ThreadSpinOnceDemo
    {
        private static int _lock = 0;
        private static int _counterA = 0;
        private static int _counterB = 0;


        public static void IncrementCounters()
        {
            var spinWait = new SpinWait();
            while (Interlocked.Exchange(ref _lock, 1) != 0)
            {
                spinWait.SpinOnce();
            }

            ++_counterA;
            ++_counterB;
            Interlocked.Exchange(ref _lock, 0);
        }

        public static void GetCounters(out int counterA, out int counterB)
        {
            var spinWait = new SpinWait();
            while (Interlocked.Exchange(ref _lock, 1) != 0)
            {
                spinWait.SpinOnce();
            }
            counterA = _counterA;
            counterB = _counterB;
            Interlocked.Exchange(ref _lock, 0);

        }
    }


}