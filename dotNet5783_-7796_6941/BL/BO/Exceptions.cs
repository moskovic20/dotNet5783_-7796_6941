using System.Runtime.Serialization;

namespace BO;

/// <summary>
/// ערך לא תקין
/// </summary>
[Serializable]
public class InvalidValue_Exception : Exception
{
    public InvalidValue_Exception() : base() { }
    public InvalidValue_Exception(string message) : base(message) { }
    public InvalidValue_Exception(string message, Exception inner) : base(message, inner) { }
    protected InvalidValue_Exception(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override string ToString()
    {
        return "InvalidValue_Exception: " + Message + "\n" + InnerException?.Message;
    }
}

/// <summary>
/// בעיה בהצגת כל המוצרים
/// </summary>
[Serializable]
public class GetAllForList_Exception : Exception
{
    public GetAllForList_Exception() : base() { }
    public GetAllForList_Exception(string message) : base(message) { }
    public GetAllForList_Exception(string message, Exception inner) : base(message, inner) { }
    protected GetAllForList_Exception(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override string ToString()
    {
        return "GetAllForList_Exception: " + Message + "\n" + InnerException?.Message;
    }
}

/// <summary>
/// בעיה בהוספת מוצר לבסיס הנתונים או לסל הקניות
/// </summary>
[Serializable]
public class Adding_Exception : Exception
{
    public Adding_Exception() : base() { }
    public Adding_Exception(string message) : base(message) { }
    public Adding_Exception(string message, Exception inner) : base(message, inner) { }
    protected Adding_Exception(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override string ToString()
    {
        return "Adding_Exception: " + Message + "\n" + InnerException?.Message;
    }
}

/// <summary>
/// בעיה בהבאת פרטי מוצר
/// </summary>
[Serializable]
public class GetDetails_Exception : Exception
{
    public GetDetails_Exception() : base() { }
    public GetDetails_Exception(string message) : base(message) { }
    public GetDetails_Exception(string message, Exception inner) : base(message, inner) { }
    protected GetDetails_Exception(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override string ToString()
    {
        return "GetDetails_Exception: " + Message + "\n" + InnerException?.Message;
    }
}

/// <summary>
/// בעיה במחיקת מוצר מבסיס הנתונים
/// </summary>
[Serializable]
public class Deleted_Exception : Exception
{
    public Deleted_Exception() : base() { }
    public Deleted_Exception(string message) : base(message) { }
    public Deleted_Exception(string message, Exception inner) : base(message, inner) { }
    protected Deleted_Exception(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override string ToString()
    {
        return "Deleted_Exception: " + Message + "\n" + InnerException?.Message;
    }
}

/// <summary>
/// בעיה בעדכון מוצר בבסיס הנתונים או בסל הקניות
/// </summary>
[Serializable]
public class Update_Exception : Exception
{
    public Update_Exception() : base() { }
    public Update_Exception(string message) : base(message) { }
    public Update_Exception(string message, Exception inner) : base(message, inner) { }
    protected Update_Exception(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override string ToString()
    {
        return "Update_Exception: " + Message + "\n" + InnerException?.Message;
    }
}

/// <summary>
/// בעיה ביצירת ההזמנה
/// </summary>
[Serializable]
public class MakeOrder_Exception : Exception
{
    public MakeOrder_Exception() : base() { }
    public MakeOrder_Exception(string message) : base(message) { }
    public MakeOrder_Exception(string message, Exception inner) : base(message, inner) { }
    protected MakeOrder_Exception(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override string ToString()
    {
        return "MakeOrder_Exception: " + Message + "\n" + InnerException?.Message;
    }

}

