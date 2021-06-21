using System;

namespace MixedLockDemo
{

    /// <summary>
    /// 相比前面的锁来说，混合锁的性能更高,任何引用类型的对象都可以做为锁对象，不需要事先创建指定类型的实例，并且设计的非托管的资源由。Net运行
    /// 时自动释放，不需要手动调用释放函数，获取和释放混合锁需要使用System.Threading.Monitor类中的函数。使用Monitor使用混合锁的例子如下：
    /// C# 调用lock语句来简化System.Threading.Monitor类获取和释放锁的代码。以下是使用lock的实例。
    /// 混合锁的特征是在获取失败后像自旋锁一样重试一定的次数，超过一定次数后再安排线程进入等待状态，
    /// 混合所的好处是，如果第一次获取锁失败，但其他线程马上释放了锁，当前线程在下一轮重试可以获取成功，不需要执行毫秒级的线程调度处理；
    /// 如果其他线程在短时间内没有释放锁，线程会在超过重试次数后进入等待状态，以避免消耗CPU资源，因此混合锁适用于大部分场景。
    /// 所有引用类型的对象都可以作为锁对象的原理是，引用类型的对象都有一个32位(4字节)的对象头，对象头的位置在对象地址之前，例如对象的内容在内存
    /// 地址中0×7fff2008时，对象头的地址在0×7fff2004。在32位的对象头中，高6位用于储存标志，低26位储存的内容根据标志而定，可以存储当前获取该锁的线程
    /// Id和进入次数(用入实现可重入)，也可以储存同步块索引。
    /// 同步块是一个包含所属线程对象，进入次数和事件对象的对象。事件对象可用于让线程进入等待状态和唤醒线程，同步块会按需要创建(如果只是用自旋锁可获取锁则无需创建)
    /// 并自动释放，.Net运行时内部有一个储存同步块的数组，同步块索引指的是同步块在这个数组中的索引.
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
