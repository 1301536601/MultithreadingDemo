using System.Threading;
using System.Collections.Generic;
using System;

namespace SemaphoreDemo
{
    /// <summary>
    /// 通过信号量演示消息队列
    /// 部分线程向这个队列添加任务，部分线程从这个队列取出任务并执行，这个模式的好处是添加任务的好处是无需等待任务处理完毕，而取出任务的线程可以有多个
    /// 多线程处理任务花费的时间变少，
    /// </summary>
    public static class ConsumerBySemaphore
    {
        private static readonly Queue<int> _queue = new();
        private static readonly object _lock = new();
        private static readonly SemaphoreSlim _sema = new(0, int.MaxValue);

        /// <summary>
        /// 消费者
        /// </summary>
        public static void Woker()
        {

            while (true)
            {
                _sema.Wait();
                lock (_lock)
                {
                    Console.WriteLine(_queue.Dequeue());
                }
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
                }
                _sema.Release();
            }
        }
    }
}