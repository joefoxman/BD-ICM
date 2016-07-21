using System;
using System.Runtime.Serialization;

namespace Bd.Icm.DataAccess
{
    public class OptimisticConcurrencyException : Exception
    {
        public string Key { get; private set; }
        public string EntityName { get; private set; }

        public OptimisticConcurrencyException()
        {

        }

        public OptimisticConcurrencyException(string entityName, string key)
        {
            EntityName = entityName;
            Key = key;
        }

        public OptimisticConcurrencyException(string entityName, string key, string message)
            : base(message)
        {
            EntityName = entityName;
            Key = key;
        }

        public OptimisticConcurrencyException(string entityName, string key, string message, Exception innerException)
            : base(message, innerException)
        {
            EntityName = entityName;
            Key = key;
        }

        public OptimisticConcurrencyException(string entityName, string key, SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            EntityName = entityName;
            Key = key;
        }
    }
}
