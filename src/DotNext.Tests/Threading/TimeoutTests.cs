using System;
using static System.Threading.Timeout;
using Xunit;

namespace DotNext.Threading
{
    public sealed class TimeoutTests : Assert
    {
        private static void InfiniteTest(Timeout timeout)
        {
            False(timeout.IsExpired);
            True(timeout.IsInfinite);
            if(timeout) throw new Xunit.Sdk.XunitException();
            Equal(InfiniteTimeSpan, timeout);
            Equal(InfiniteTimeSpan, timeout.RemainingTime);
        }

        [Fact]
        public static void DefaultValue()
        {
            InfiniteTest(default);
            InfiniteTest(new Timeout(InfiniteTimeSpan));
        }       
    }
}