
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace Do;

//כלומר תהיינה חריגות כלליות לכל הישויות.מומלצות בשכבת הנתונים שתי החריגות הבאות (כי בעצם אלו הדברים היחידים שנבדקים בלוגיקת השכבה):

//חריגה של כפילות מזהה(עבור הוספה של אובייקט עם מזהה שכבר קיים)
//[Serializable]
//public class DuplicationException : Exception
//{
//    public int ID;
//    public DuplicationException(int id) : base() => ID = id;
//    public DuplicationException(string message) : base(message) { }
//    public DuplicationException(string message, Exception innerException) : base(message, innerException) { }
//    protected DuplicationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
//    public override string Message => "DUPLICATIONS";

//    public string Massege { get; private set; }

//    public override string ToString()
//    {
//        return Massege;
//    }

//}

//[Serializable]
//public class BadPersonIdException : Exception
//{
//    public int ID;
//    public BadPersonIdException(int id) : base() => ID = id;
//    public BadPersonIdException(int id, string message) :
//        base(message) => ID = id;
//    public BadPersonIdException(int id, string message, Exception innerException) :
//        base(message, innerException) => ID = id;
//    public override string ToString() => base.ToString() + $", bad person id: {ID}";
//}


//חריגה עבור ישות שלא נמצאה או מזהה חסר(עבור עדכון, מחיקה או בקשה)
[Serializable]
public class NotFounfException : Exception
{
    public NotFounfException() : base() { }
    public NotFounfException(string message) : base(message) { }
    public NotFounfException(string message, Exception innerException) : base(message, innerException) { }
    protected NotFounfException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    //public override string Message => "NOTFOUND";

    //public string Massege { get; private set; }

    //public override string ToString()
    //{
    //    return Massege;
    //}

}

public class AlreadyExistException : Exception
{
    public AlreadyExistException() : base() { }
    public AlreadyExistException(string message) : base(message) { }
    public AlreadyExistException(string message, Exception inner) : base(message, inner) { }
    protected AlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}

//[Serializable]
//public class BadPersonIdCourseIDException : Exception
//{
//    public int personID;
//    public int courseID;
//    public BadPersonIdCourseIDException(int perID, int crsID) : base() { personID = perID; courseID = crsID; }
//    public BadPersonIdCourseIDException(int perID, int crsID, string message) :
//        base(message)
//    { personID = perID; courseID = crsID; }
//    public BadPersonIdCourseIDException(int perID, int crsID, string message, Exception innerException) :
//        base(message, innerException)
//    { personID = perID; courseID = crsID; }
//    public override string ToString() => base.ToString() + $", bad person id: {personID} and course id: {courseID}";
//}
