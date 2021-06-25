using System.Collections.Generic;
using System;
using System.Threading;

namespace SemaphoreDemo
{
    /// <summary>
    /// 通过Monitor演示消息队列
    /// 通过信号量实现消费者模式，在没出执行出队操作之前都必须执行减少数量操作，所以性能比较低，
    /// 而在Monitor中不仅包含线程锁的功能，还包含条件变量功能，条件变量有唤醒和等待两个操作，线程执行等待操作可以进入等待状态，执行唤醒操作
    /// 可以唤醒单个或者全部等待状态的线程
    /// 条件变量的特征是，执行等待操作时需要在已获取锁的状态下执行，等待操作会先添加当前线程到等待队列，再释放锁并进入等待状态。唤醒后再重新获取锁。
    /// 这样的处理流程保证了检查条件和执行处理收到线程锁的保护，并且当条件成立是可以不执行等待操作。
    /// TODO: 不需要记录在博客中
    /// .Net不允许释放锁后执行唤醒操作，
    /// </summary>
    public static class ConsumerByMonitorDemo
    {

        private static readonly Queue<int> _queue = new();
        private static readonly object _lock = new();

        /// <summary>
        /// 消费者
        /// </summary>
        public static void Woker()
        {

            lock (_lock)
            {
                while (_queue.Count == 0)
                {
                    Monitor.Wait(_lock);
                }
                Console.WriteLine(_queue.Dequeue());
            }
        }

        public static void DoWork()
        {
            for (int i = 0; i < 6; i++)
            {
                var thread = new Thread(Woker);
                thread.Start();
            }
            var job = 0;
            while (true)
            {
                lock (_lock)
                {
                    _queue.Enqueue(job++);
                    Monitor.Pulse(_lock);
                }
                Thread.Sleep(1000);
            }
        }
    }
}