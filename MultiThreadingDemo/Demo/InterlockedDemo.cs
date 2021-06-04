using System.Threading;

namespace MultiThreadingDemo.Demo
{
    public static class InterlockedDemo
    {
        public static long IncrementNum = 0;

        /// <summary>
        /// 自增
        /// </summary>
        public static void Increment()
        {
            Interlocked.Increment(ref IncrementNum);
        }

        /// <summary>
        /// 自减
        /// </summary>
        public static void Decrement()
        {
            Interlocked.Decrement(ref IncrementNum);
        }

        /// <summary>
        /// IncrementNum=IncrementNum+number
        /// </summary>
        /// <param name="number"></param>
        public static void Add(int number)
        {
            Interlocked.Add(ref IncrementNum, number);
        }

        /// <summary>
        /// 取值时先取 IncrementNum的值 在下次取值时得到的值为上一次的 IncrementNum+number
        /// </summary>
        /// <param name="number"></param>
        public static long Exchange(int number)
        {
            var oldValue = Interlocked.Exchange(ref IncrementNum, number);
            return oldValue;
        }

        /// <summary>
        /// 先对比当前的contrastNumber和当前的 IncrementNum 如果相等 则 IncrementNum等于number；
        /// </summary>
        /// <param name="number"></param>
        /// <param name="contrastNumber"></param>
        public static void CompareExchange(int number, int contrastNumber)
        {
            Interlocked.CompareExchange(ref IncrementNum, number, contrastNumber);
        }

        /// <summary>
        /// 读取值
        /// </summary>
        public static long Read()
        {
            var number = Interlocked.Read(ref IncrementNum);
            return number;
        }
    }
}