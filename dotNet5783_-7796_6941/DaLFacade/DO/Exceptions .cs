using System.Runtime.Serialization;
namespace Do;


/// <summary>
/// חריגה עבור ישות שלא נמצאה או מזהה חסר עבור עדכון, מחיקה או בקשה
/// </summary>
[Serializable]
public class DoesntExistException : Exception
{
    public DoesntExistException() : base() { }
    public DoesntExistException(string message) : base(message) { }
    public DoesntExistException(string message, Exception innerException) : base(message, innerException) { }
    protected DoesntExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}

/// <summary>
/// חריגה עבור כפל בקשה למשהו שכבר קיים
/// </summary>
public class AlreadyExistException : Exception
{
    public AlreadyExistException() : base() { }
    public AlreadyExistException(string message) : base(message) { }
    public AlreadyExistException(string message, Exception inner) : base(message, inner) { }
    protected AlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}


/// <summary>
/// LoadingException
/// </summary>
public class LoadingException : Exception
{
    string? filePath;
    public LoadingException() : base() { }
    public LoadingException(string message) : base(message) { }
    public LoadingException(string message, Exception inner) : base(message, inner) { }

    public LoadingException(string path, string messege, Exception inner) => filePath = path;
    protected LoadingException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}

