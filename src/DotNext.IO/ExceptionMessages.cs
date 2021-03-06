﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Resources;

namespace DotNext
{
    [SuppressMessage("Globalization", "CA1304", Justification = "This is culture-specific resource strings")]
    [SuppressMessage("Globalization", "CA1305", Justification = "This is culture-specific resource strings")]
    [ExcludeFromCodeCoverage]
    internal static class ExceptionMessages
    {
        private static readonly ResourceManager Resources = new ResourceManager("DotNext.ExceptionMessages", Assembly.GetExecutingAssembly());

        internal static string BufferTooSmall => Resources.GetString("BufferTooSmall");

        internal static string StreamNotWritable => Resources.GetString("StreamNotWritable");

        internal static string DirectoryNotFound(string path)
            => string.Format(Resources.GetString("DirectoryNotFound"), path);

        internal static string WriterInReadMode => Resources.GetString("WriterInReadMode");
    }
}