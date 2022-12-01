
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace Do;


//חריגה עבור ישות שלא נמצאה או מזהה חסר עבור עדכון, מחיקה או בקשה)

[Serializable]
public class DoesntExistException : Exception
{
    public DoesntExistException() : base() { }
    public DoesntExistException(string message) : base(message) { }
    public DoesntExistException(string message, Exception innerException) : base(message, innerException) { }
    protected DoesntExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}

public class AlreadyExistException : Exception
{
    public AlreadyExistException() : base() { }
    public AlreadyExistException(string message) : base(message) { }
    public AlreadyExistException(string message, Exception inner) : base(message, inner) { }
    protected AlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}
