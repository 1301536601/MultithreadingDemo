using System;
using System.Threading;

namespace TheMutexDemo
{
    /// <summary>
    /// 和自旋锁一样，操作系统提供的互斥锁内部有一个数值表示锁是否已经被获取，不同的是当获取锁失败的时候，它不会反复进行重试，而且让线程进入等待状态，并把线程
    /// 对象添加到锁关联的队列中，另一个线程释放锁时会检查队列中是否有线程对象，如果有则通知操作系统唤醒该线程，因为获取锁的线程对象没有进行运行，即使锁长时间不释放
    /// 也不会消耗CPU资源，但让线程进入等待状态和从等待状态唤醒的时间比自旋锁重试的纳秒级时间要长
    /// 在windows和linux上的区别
    /// 在windows系统上互斥锁通过CreateMuteEx函数创建，获取锁时将调用WaitForMultipleObjectsEx函数，释放锁将调用ReleaseMutex函数，线程进入等待状态和唤醒由系统操作
    /// 在Linux上互斥锁对象由NetCore的内部接口模拟实现，结果包含锁的状态值以及等待线程队列，每个托管线程都会关联一个pthread_mutex_t对象和一个pthread_cond_t对象，这两个对象
    /// 友pthread类库提供，获取锁失败线程会调价到队列pthread_cond_wait函数等待，另一个线程释放锁时看到队列中有线程则调用pthread_cond_signal函数唤醒。
    /// Mutex提供的锁可重入，已经获取锁的线程可以再次执行获取锁的操作，但释放锁的操作也要执行对应的相同次数，可重入的锁又叫递归锁。
    ///
    /// Mutex 支持夸进程使用，创建是通过构造函数的第二个参数name传入名称，名称以local开始时同一个用户的进程共享拥有此名称的锁，名称以"Global"开始时同一台计算机的进程拥有
    /// 此名称的锁，如果一个进程中获取了锁，那么在释放该锁前另一个进程获取同样名称的锁需要等待，如果进程获取了锁，但是在退出之前没有调用释放锁的方法，那么锁会被自动释放，
    /// 其他当前正在等待锁的京城会受到AbandonedMuteException异常。实现方式都是通过临时文件的方式实现
    /// </summary>
    public static class MutexDemo
    {
        private static readonly Mutex _lock = new Mutex(false, null);
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


    public static class MutexRecursionDemo
    {
        private static Mutex _lock = new Mutex(false, null);
        private static int _counterA = 0;
        private static int _counterB = 0;

        public static void IncrementCountersA()
        {
            //获取锁
            _lock.WaitOne();
            try
            {
                ++_counterA;
            }
            finally
            {
                //释放锁
                _lock.ReleaseMutex();
            }
        }

        public static void IncrementCountersB()
        {
            //获取锁
            _lock.WaitOne();
            try
            {
                ++_counterB;
            }
            finally
            {
                //释放锁
                _lock.ReleaseMutex();
            }
        }

        public static void IncrementCounters()
        {
            //获取锁
            _lock.WaitOne();
            try
            {
                IncrementCountersA();
                IncrementCountersB();
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