using System;

namespace Adventure.Foundation.Common.Exceptions
{
    public class CannotDisplayComponentException : Exception
    {
        public CannotDisplayComponentException(string messageDictionaryKey)
        {
            MessageDictionaryKey = messageDictionaryKey;
        }

        public string MessageDictionaryKey { get; set; }
    }
}