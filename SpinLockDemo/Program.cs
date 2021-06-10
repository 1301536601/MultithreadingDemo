using System;

namespace SpinLockDemo
{
    /// <summary>
    /// Thread.Sleep(0)和Thread.Yield的区别
    /// 在Windows系统中 Thread.Sleep调用系统提供的SleepEx函数，Thread.Yield函数调用的是系统提供的SwitchToThread方法，
    /// 区别在于SwitchToThread函数只会切换到当前核心逻辑关联的待运行队列的线程，不会切换到其他核心逻辑关联的线程上，
    /// 而SleepEx函数会切换到任意逻辑核心关联的待运行队列中的线程，并且让当前线程在指定时间内无法重新进入待运行队列(如果线程为0 那么线程可以立刻重新进入待运行队列)
    /// 在Linux和OSX中 Thread.Sleep函数在休眠时间不为0时会调用pthread类库提供的pthread_cond_timedWait函数，在休眠时间为0时会调用sched_yield函数
    /// Thread.Yield同样会调用sched_yield函数 sched_yield在windows和osx系统中没有区别，都只会切换到当前和逻辑核心关心的待运行队列中的线程，不会切换到其他核心逻辑关联的线程上
    /// 在unix系统上调用系统提供的sleep函数并传入0 会直接忽略返回
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
