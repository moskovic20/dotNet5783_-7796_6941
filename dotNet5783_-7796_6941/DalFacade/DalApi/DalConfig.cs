namespace DalApi;
using System.Xml.Linq;

static class DalConfig
{
    internal static string? s_dalName;//במקרה שלנו בחרנו בנתיים דלליסו מתוך 2 האופציות
    internal static Dictionary<string, string> s_dalPackages;//אוסף כל האופציות בצורה ממויינת במילון

    static DalConfig()//בנאי
    {
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml")//שם הקובץ שאותו הוא יטען והדרך אליו
            ?? throw new DalConfigException("dal-config.xml file is not found");
        s_dalName = dalConfig?.Element("dal")?.Value //  ( בתוך העץ שהגדרנו בקובץ יש שם את דלליסט (שבתוך מילת מפתח "ליסט" ובתוכו הדלליסט המבוקש"dal" בעזרת מילת המפתח
            ?? throw new DalConfigException("<dal> element is missing");
        var packages = dalConfig?.Element("dal-packages")?.Elements() //  xmlתטען את כל סוגי הקבצים ששמנו בדלקופיג- אצלינו זה דלליסט ודל  
            ?? throw new DalConfigException("<dal-packages> element is missing");
        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Value);// שים אותם במילון שאוכל לשלוף עם מפתח מבוקש כך{ "list": "DalList","xml": "DalXml"}

    }
}

[Serializable]
public class DalConfigException : Exception //חריגות שהקובץ הנ"ל משתמש בהן
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}
