using System;
using System.Threading;

using MultiThreadingDemo.Demo;

namespace MultiThreadingDemo
{

    /// <summary>
    /// 多线程 原子性操作Demo
    /// </summary>
    class Program
    {
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

            var slot = Thread.AllocateNamedDataSlot("slot");
            Thread.SetData(slot, "localDataStoreSlotDemo");

            var th = new Thread(() =>
            {
                Thread.SetData(slot, "DiDi"); // 假设新开线程不设置ThreadStaticNum 则slot为默认值
                ShowLocalDataStoreSlot();
            });
            th.Start();
            th.Join();

            Console.WriteLine($@"MainThreadId Is {Thread.CurrentThread.ManagedThreadId} Current Result Is {Thread.GetData(slot)}");


            Thread.FreeNamedDataSlot("localDataStoreSlotDemo");
            #endregion

            Console.ReadKey();
        }

        public static void ShowLocalDataStoreSlot()
        {
            LocalDataStoreSlot dataSlot = Thread.GetNamedDataSlot("slot");
            Console.WriteLine($@"Current Is New Thread ,ThreadId Is {Thread.CurrentThread.ManagedThreadId} Current Result Is {Thread.GetData(dataSlot)}");
        }
    }
}
