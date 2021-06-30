using System;
using System.Collections.Generic;
using System.Threading;

namespace ReadWriteLockDemo
{
    /// <summary>
    /// RederWtireLockSiml由C#代码实现，它也是一个混合锁(Hybird Lock)，在获取锁时通过自旋重试一定次数再次进入等待状态，进入等待状态使用的是
    /// 事件对象(与同步快内部使用的事情对象一样)。此外，还支持同一个线程先获取读取锁，然后再升级为写入锁，适用于“需要先获取读取锁，然后读取
    /// 贡献数据是否须要修改，需要修改时再获取写入锁”的场景，在C#先获取读取锁再升级为写入锁的例子，
    /// </summary>
    public static class ReadWriteLockDemo
    {
        private static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private static Dictionary<string, string> _dict = new Dictionary<string, string>();

        public static string GetValue(string key, Func<string, string> factory)
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                //值已生成时可以直接返回
                if (_dict.TryGetValue(key, out var value))
                {
                    return value;
                }
                //获取(升级到)写入锁
                _lock.EnterWriteLock();
                try
                {
                    //再次判断值是否已生成
                    if (!_dict.TryGetValue(key, out value))
                    {
                        value = factory(key);
                        _dict.Add(key, value);
                    }
                    return value;
                }
                finally
                {
                    //释放写入锁
                    _lock.ExitWriteLock();
                }
            }
            finally
            {
                //释放读取锁
                _lock.ExitUpgradeableReadLock();
            }
        }
    }
}