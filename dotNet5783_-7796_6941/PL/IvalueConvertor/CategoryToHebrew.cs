using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.IvalueConvertor
{
    public class CategoryToHebrew : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PO.CATEGORY myCategory = (PO.CATEGORY)value;

            if (myCategory == PO.CATEGORY.mystery)
                return PO.Hebrew_CATEGORY.מסתורין;
            if (myCategory == PO.CATEGORY.fantasy)
                return PO.Hebrew_CATEGORY.פנטזיה ;
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
