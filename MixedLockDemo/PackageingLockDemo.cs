namespace MixedLockDemo
{
    /// <summary>
    /// 封装后的lock语句使用
    /// </summary>
    public static class PackageingLockDemo
    {
        private static readonly object _lock = new();
        private static int _counterA = 0;
        private static int _counterB = 0;

        /// <summary>
        /// 增加
        /// </summary>
        public static void IncrementCounters()
        {
            lock (_lock)
            {
                ++_counterA;
                ++_counterB;
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="counterA"></param>
        /// <param name="coubterB"></param>
        public static void GetCounters(ref int counterA, ref int coubterB)
        {
            lock (_lock)
            {
                counterA = _counterA;
                coubterB = _counterB;
            }
        }
    }
}