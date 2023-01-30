﻿namespace DalApi;
using System.Reflection;
using static DalApi.DalConfig;

public static class Factory
{
    public static IDal? Get() //  פונקציית גט שתביא לנו את המחלקה הנ"ל ומי שבתוכה אבל זה מביא רק את החוזים בתכלס
    {
        string dalType = s_dalName // ייבא את דלאקסמל/דלליסט ועכשיו יש לנו מימוש ספציפי של החוזה בתור דלאקסמל/דלליסט // xml שם הקובץ ששיכנו אליו ע"י הדלקונפיג שבקובץ 
            ?? throw new Do.DalConfigException($"DAL name is not extracted from the configuration");
        string dal = s_dalPackages[dalType]// מתוך המילון לקחנו את הטייפ המבוקש לנו שכרגע זה דלליסט ועכשיו כל פעם שנקרא לדל הכוונה לדלליסט
           ?? throw new Do.DalConfigException($"Package for {dalType} is not found in packages list");

        try
        {
            Assembly.Load(dal ?? throw new Do.DalConfigException($"Package {dal} is null"));//  DalList.dll -תטען את קובץ מהתקיית בין במקרה ההתחלתי שלנו  

        }
        catch (Exception)
        {
            throw new Do.DalConfigException("Failed to load {dal}.dll package");
        }

        Type? type = Type.GetType($"Dal.{dal}, {dal}")
            ?? throw new Do.DalConfigException($"Class Dal.{dal} was not found in {dal}.dll");

        return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?
                   .GetValue(null) as IDal
            ?? throw new Do.DalConfigException($"Class {dal} is not singleton or Instance property not found");
    }
}
