using System;

namespace Bd.Icm.Core
{
    public sealed class StringValueAttribute : Attribute
    {
        public string Value { get; private set; }

        public StringValueAttribute(string value)
        {
            Value = value;
        }
    }
}
