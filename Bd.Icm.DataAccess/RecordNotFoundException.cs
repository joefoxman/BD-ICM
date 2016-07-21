using System;
using System.Runtime.Serialization;

namespace Bd.Icm.DataAccess
{
    public class RecordNotFoundException : Exception
    {
        public string Key { get; private set; }
        public string EntityName { get; private set; }

        public RecordNotFoundException(string entityName, string key)
            : base()
        {
            Key = key;
            EntityName = entityName;
        }

        public RecordNotFoundException(string entityName, string key, string message)
            : base(message)
        {
            Key = key;
            EntityName = entityName;
        }

        public RecordNotFoundException(string entityName, string key, string message, Exception innerException)
            : base(message, innerException)
        {
            Key = key;
            EntityName = entityName;
        }

        public RecordNotFoundException(string entityName, string key, SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Key = key;
            EntityName = entityName;
        }
    }
}
