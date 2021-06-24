using System.Threading;
using System;

namespace SemaphoreDemo
{
    /// <summary>
    /// 信号量
    /// 事例：大家去银行去银行取钱，互斥锁管理的时一个柜台是否正在处理业务，而信号量管理的是整个柜台是否正在处理业务，每当有一个柜台处理完成之后，
    /// A大堂经理则进行叫号喊下一位进行处理业务，B大堂经理则对进来的客户进行接待，当柜台全部都在办理业务时，新来的办理业务者则需要进行等待
    /// 信号量和互斥锁的区别：
    /// 互斥锁释放锁的线程必须是获取锁的线程，而信号量增加数量和减少数量可以是不通
    /// 信号锁的基础概念
    /// 信号量是一个具有特殊用途的线程同步对象，相比互斥锁只有两个状态(未被获取/已被获取)，信号量内部使用一个数值记录可用的数量，每个线程可以
    /// 通过增加和减少数量两个操作进行同步。
    /// 当执行减少数量操作时，如果减少的数量大于现有的数量，则线程需要进入等待状态，知道其他线程执行增加数量操作后数量不少于减少的数量为止。
    /// 操作系统的区别：
    /// windows系统中信号量对象从CreateSemaphoreEx函数创建，减少数量是通过WaitForMultipObjectsEx函数，增加数量时通过ReleaseSemaphoehanshu,
    /// 由于接口限制，减少数量时只能减少1，而增加数量则可以使用自定义数量。
    /// linux系统中有Net Core的内部结构模拟实现。
    /// 和System.Threading.Mutex一样，Semaphore可以进行使用参数命名来控制跨进程使用(注意，只支持window平台，其他平台使用会抛出异常)
    /// 
    /// </summary>
    public class Program
    {
        //第一个参数代表初始数量，第二个参数代表最大数量,第三个参数代表跨进程名称
        private static readonly Semaphore _sema = new(0, 10);

        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            for (var i = 0; i < 6; ++i)
            {
                var thread = new Thread(Work)
                {
                    IsBackground = true
                };
                thread.Start();
            }
            while (true)
            {
                //执行增加数量，增加值为5
                _sema.Release(5);
                Thread.Sleep(1000);
            }
        }

        public static void Work()
        {
            while (true)
            {
                //执行减少操作，减少值为1
                _sema.WaitOne();
                Console.WriteLine($"当前时间为：{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}");
                Console.WriteLine($"当前线程Id为:{Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine("************************");
            }
        }
    }
}
