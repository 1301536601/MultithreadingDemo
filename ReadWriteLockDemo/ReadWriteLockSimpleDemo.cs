using System.Threading;
namespace ReadWriteLockDemo
{
    public static class ReadWriteLockSimpleDemo
    {
        private static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private static int _countA = 0;

        public static int _countB = 0;

        /// <summary>
        /// 增加
        /// </summary>
        public static void IncrementCounters()
        {
            _lock.EnterWriteLock();
            try
            {
                ++_countA;
                ++_countB;
            }
            finally
            {

                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="countA"></param>
        /// <param name="countB"></param>
        public static void GetCounters(ref int countA, ref int countB)
        {
            _lock.EnterReadLock();
            try
            {
                countA = _countA;
                countB = _countB;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }


}