namespace System.Runtime.Serialization
{
    /***********************************************************
     *
     * These were added to help the porting from .NET to .NET CF
     * - Pekka Heikura
     * 
     * ********************************************************/

    using OpenTK;

    public interface ISerializable
    {
        void GetObjectData(SerializationInfo info, StreamingContext context);
    }

    public class SerializationInfo
    {
        public Half GetValue(string name, Type type)
        {
            throw new NotImplementedException();
        }

        public void AddValue(string name, object value)
        {
            throw new NotImplementedException();
        }
    }

    public class StreamingContext
    {
        
    }
}
