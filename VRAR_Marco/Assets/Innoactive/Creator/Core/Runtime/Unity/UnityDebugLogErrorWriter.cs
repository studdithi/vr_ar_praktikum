﻿using System.IO;
using System.Text;
using UnityEngine;

namespace Innoactive.Creator.Unity
{
    /// <summary>
    /// A TextWriter implementation that outputs any added text to Unity's DebugLogger
    /// </summary>
    public class UnityDebugLogErrorWriter : TextWriter
    {
        /// <inheritdoc />
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        private readonly StringBuilder buffer = new StringBuilder();

        /// <inheritdoc />
        public override void Flush()
        {
            Debug.LogError(buffer.ToString());
            buffer.Length = 0;
        }

        /// <inheritdoc />
        public override void Write(string value)
        {
            buffer.Append(value);
            if (value == null)
            {
                return;
            }
            int length = value.Length;
            if (length <= 0)
            {
                return;
            }
            char lastChar = value [length - 1];
            if (lastChar == '\n')
            {
                Flush();
            }
        }

        /// <inheritdoc />
        public override void Write(char value)
        {
            buffer.Append(value);
            if (value == '\n')
            {
                Flush();
            }
        }

        /// <inheritdoc />
        public override void Write(char[] value, int index, int count)
        {
            Write(new string (value, index, count));
        }
    }
}
