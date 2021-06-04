using System;
using System.Linq;
using System.Threading;

using MultiThreadingDemo.Demo;

namespace MultiThreadingDemo
{

    /// <summary>
    /// 多线程 原子性操作Demo
    /// </summary>
    class Program
    {
        public static LocalDataStoreSlot Slot;

        public static ThreadLocal<string> Local;

        static void Main(string[] args)
        {

            #region ThreadStaticDemo

            //var thread = new Thread(() =>
            //{
            //    //ThreadStaticDemo.ThreadStaticNum = 1;//假设新开线程不设置ThreadStaticNum 则ThreadStaticNum初始默认值为0
            //    ThreadStaticDemo.Show();
            //});
            //thread.Start();
            //thread.Join();

            //Console.WriteLine($@"MainThreadId Is {Thread.CurrentThread.ManagedThreadId} Current ThreadNum Is {ThreadStaticDemo.ThreadStaticNum}");
            //Console.WriteLine("*******This Is ThreadStatic********");

            #endregion

            #region LocalDataStoreSlotDemo

            //var slot = Thread.AllocateNamedDataSlot("slot");
            //Thread.SetData(slot, "localDataStoreSlotDemo");

            //var th = new Thread(() =>
            //{
            //    Thread.SetData(slot, "DiDi"); // 假设新开线程不设置ThreadStaticNum 则slot为默认值
            //    ShowLocalDataStoreSlot();
            //});
            //th.Start();
            //th.Join();

            //Console.WriteLine($@"MainThreadId Is {Thread.CurrentThread.ManagedThreadId} Current Result Is {Thread.GetData(slot)}");


            //Thread.FreeNamedDataSlot("localDataStoreSlotDemo");
            #endregion

            #region 未命名LocalDataStoreSlot

            //Slot = Thread.AllocateDataSlot();

            ////设置TLS中的值
            //Thread.SetData(Slot, "hehe");

            ////修改TLS的线程
            //var th = new Thread(() =>
            //{
            //    Thread.SetData(Slot, "Mgen");
            //    UnNamedShowLocalDataStoreSlot();
            //});

            //th.Start();
            //th.Join();
            //Console.WriteLine($@"MainThreadId Is {Thread.CurrentThread.ManagedThreadId} Current Result Is {Thread.GetData(Slot)}");

            #endregion

            #region ThreadLocal

            //Local = new ThreadLocal<string>(() => "hehe");
            //var th = new Thread(() =>
            //{
            //    Local.Value = "Mgen";
            //    ShowThreadLocal();
            //});

            //th.Start();
            //th.Join();
            //Console.WriteLine($@"MainThreadId Is {Thread.CurrentThread.ManagedThreadId} Current Result Is {Local.Value}");

            #endregion

            #region Interlocked

            var oldValue = InterlockedDemo.Exchange(10);
            Console.WriteLine($@"*******oldValue={oldValue}**********");
            InterlockedDemo.Add(10);
            InterlockedDemo.Increment();
            InterlockedDemo.Decrement();
            InterlockedDemo.CompareExchange(30, 20);
            Console.WriteLine(InterlockedDemo.Read());

            #endregion

            Console.ReadKey();
        }

        public static void ShowLocalDataStoreSlot()
        {
            LocalDataStoreSlot dataSlot = Thread.GetNamedDataSlot("slot");
            Console.WriteLine($@"Current Is New Thread ,ThreadId Is {Thread.CurrentThread.ManagedThreadId} Current Result Is {Thread.GetData(dataSlot)}");
        }

        public static void UnNamedShowLocalDataStoreSlot()
        {
            Console.WriteLine($@"Current Is New Thread ,ThreadId Is {Thread.CurrentThread.ManagedThreadId} Current Result Is {Thread.GetData(Slot)}");
        }

        public static void ShowThreadLocal()
        {
            Console.WriteLine($@"Current Is New Thread ,ThreadId Is {Thread.CurrentThread.ManagedThreadId} Current Result Is {Local.Value}");
        }
    }
}
