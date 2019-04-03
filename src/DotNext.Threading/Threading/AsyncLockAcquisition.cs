using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace DotNext.Threading
{
    /// <summary>
    /// Provides a set of methods to acquire different types of asynchronous lock.
    /// </summary>
    public static class AsyncLockAcquisition
    {
        private static readonly UserDataSlot<AsyncReaderWriterLock> ReaderWriterLock = UserDataSlot<AsyncReaderWriterLock>.Allocate();
        private static readonly UserDataSlot<AsyncLock> ExclusiveLock = UserDataSlot<AsyncLock>.Allocate();

        private static Task<AsyncLock.Holder> Acquire<T>(T obj, Converter<T, AsyncLock> locker, TimeSpan timeout)
            where T : class
            => locker(obj).Acquire(timeout);

        private static Task<AsyncLock.Holder> Acquire<T>(T obj, Converter<T, AsyncLock> locker, CancellationToken token)
            where T : class
            => locker(obj).Acquire(token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static AsyncReaderWriterLock GetReaderWriterLock<T>(this T obj)
            where T : class
            => obj.GetUserData().GetOrSet(ReaderWriterLock, () => new AsyncReaderWriterLock());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static AsyncLock GetExclusiveLock<T>(this T obj)
            where T : class
            => obj.GetUserData().GetOrSet(ExclusiveLock, AsyncLock.Exclusive);

        public static Task<AsyncLock.Holder> LockAsync<T>(this T obj, TimeSpan timeout) where T : class => Acquire(obj, GetExclusiveLock, timeout);

        public static Task<AsyncLock.Holder> LockAsync<T>(this T obj, CancellationToken token) where T : class => Acquire(obj, GetExclusiveLock, token);

        public static Task<AsyncLock.Holder> ReadLockAsync(this AsyncReaderWriterLock @lock, TimeSpan timeout) => Acquire(@lock, AsyncLock.ReadLock, timeout);

        public static Task<AsyncLock.Holder> ReadLockAsync(this AsyncReaderWriterLock @lock, CancellationToken token) => Acquire(@lock, AsyncLock.ReadLock, token);

        public static Task<AsyncLock.Holder> ReadLockAsync<T>(this T obj, TimeSpan timeout) where T : class => obj.GetReaderWriterLock().ReadLockAsync(timeout);

        public static Task<AsyncLock.Holder> ReadLockAsync<T>(this T obj, CancellationToken token) where T : class => obj.GetReaderWriterLock().ReadLockAsync(token);
        
    }
}