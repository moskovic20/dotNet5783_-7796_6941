using PL.PO;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.IvalueConvertor;

public class CategoryToHebrew : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        PO.CATEGORY myCategory = (PO.CATEGORY)value;

        if (myCategory == PO.CATEGORY.mystery)
            return PO.Hebrew_CATEGORY.מסתורין;
        if (myCategory == PO.CATEGORY.fantasy)
            return PO.Hebrew_CATEGORY.פנטזיה;
        if (myCategory == PO.CATEGORY.history)
            return PO.Hebrew_CATEGORY.היסטוריה;
        if (myCategory == PO.CATEGORY.scinence)
            return PO.Hebrew_CATEGORY.מדע;
        if (myCategory == PO.CATEGORY.childen)
            return PO.Hebrew_CATEGORY.ילדים;
        if (myCategory == PO.CATEGORY.romans)
            return PO.Hebrew_CATEGORY.רומן;
        if (myCategory == PO.CATEGORY.cookingAndBaking)
            return PO.Hebrew_CATEGORY.בישול_ואפייה;
        if (myCategory == PO.CATEGORY.psychology)
            return PO.Hebrew_CATEGORY.פסיכולוגיה;

        return PO.Hebrew_CATEGORY.קודש;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        PO.Hebrew_CATEGORY hebrewCategory = (PO.Hebrew_CATEGORY)value;

        if (hebrewCategory == Hebrew_CATEGORY.מסתורין)
            return PO.CATEGORY.mystery;
        if (hebrewCategory == Hebrew_CATEGORY.פנטזיה)
            return PO.CATEGORY.fantasy;
        if (hebrewCategory == Hebrew_CATEGORY.היסטוריה)
            return PO.CATEGORY.history;
        if (hebrewCategory == Hebrew_CATEGORY.מדע)
            return PO.CATEGORY.scinence;
        if (hebrewCategory == Hebrew_CATEGORY.ילדים)
            return PO.CATEGORY.childen;
        if (hebrewCategory == Hebrew_CATEGORY.רומן)
            return PO.CATEGORY.romans;
        if (hebrewCategory == Hebrew_CATEGORY.בישול_ואפייה)
            return PO.CATEGORY.cookingAndBaking;
        if (hebrewCategory == Hebrew_CATEGORY.פסיכולוגיה)
            return PO.CATEGORY.psychology;

        return PO.CATEGORY.Kodesh;
    }
}
