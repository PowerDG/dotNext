using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using static System.Globalization.CultureInfo;

namespace DotNext.IO
{
    using static Buffers.BufferWriter;

    [ExcludeFromCodeCoverage]
    public sealed class TextWriterSourceTests : Test
    {
        [Fact]
        public static void WriteText()
        {
            var writer = new ArrayBufferWriter<char>();
            using var actual = writer.AsTextWriter();
            
            using TextWriter expected = new StringWriter(InvariantCulture);

            actual.Write("Hello, world!");
            expected.Write("Hello, world!");

            actual.Write("123".AsSpan());
            expected.Write("123".AsSpan());

            actual.Write(TimeSpan.Zero);
            expected.Write(TimeSpan.Zero);

            actual.Write(true);
            expected.Write(true);

            actual.Write('a');
            expected.Write('a');

            actual.Write(20);
            expected.Write(20);

            actual.Write(20U);
            expected.Write(20U);

            actual.Write(42L);
            expected.Write(42L);

            actual.Write(46UL);
            expected.Write(46UL);

            actual.Write(89M);
            expected.Write(89M);

            actual.Write(78.8F);
            expected.Write(78.8F);

            actual.Write(90.9D);
            expected.Write(90.9D);

            actual.WriteLine();
            expected.WriteLine();

            Equal(expected.ToString(), writer.BuildString());
        }

        [Fact]
        public static async Task WriteTextAsync()
        {
            var writer = new ArrayBufferWriter<char>();
            using var actual = writer.AsTextWriter();
            
            using TextWriter expected = new StringWriter(InvariantCulture);

            await actual.WriteAsync("Hello, world!");
            await expected.WriteAsync("Hello, world!");

            await actual.WriteAsync("123".AsMemory());
            await expected.WriteAsync("123".AsMemory());

            await actual.WriteAsync('a');
            await expected.WriteAsync('a');

            await actual.WriteLineAsync();
            await expected.WriteLineAsync();

            Equal(expected.ToString(), writer.BuildString());
        }
    }
}