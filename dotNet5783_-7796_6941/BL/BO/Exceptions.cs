using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO;
[Serializable]
public class NotFounfException : Exception
{
    public NotFounfException() : base() { }
    public NotFounfException(string message) : base(message) { }
    public NotFounfException(string message, Exception innerException) : base(message, innerException) { }
    protected NotFounfException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}

public class AlreadyExistException : Exception
{
    public AlreadyExistException() : base() { }
    public AlreadyExistException(string message) : base(message) { }
    public AlreadyExistException(string message, Exception inner) : base(message, inner) { }
    protected AlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}
