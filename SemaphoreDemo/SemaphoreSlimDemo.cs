using System.Threading;
namespace SemaphoreDemo
{
    /// <summary>
    /// 轻量信号量Demo
    /// 轻量信号量不支持跨进程使用，如果不需要使用跨进程，可以使用SemaphoreSlim来代替Semaphore
    /// </summary>
    public class SemaphoreSlimDemo
    {
        private static readonly SemaphoreSlim _semaphoreSlim = new(0, 10);

        public static void Wrok()
        {
            while (true)
            {
                _semaphoreSlim.Wait();
                System.Console.WriteLine("do work");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Run()
        {
            for (var i = 0; i < 6; i++)
            {
                var thread = new Thread(Wrok)
                {
                    IsBackground = true
                };
                thread.Start();
            }
            while (true)
            {
                _semaphoreSlim.Release(2);
                Thread.Sleep(1000);
            }
        }
    }
}