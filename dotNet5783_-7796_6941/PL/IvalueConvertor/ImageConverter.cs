using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PL.IvalueConvertor
{
    class ImageConverter : IValueConverter//ממיר את הנתיב שאנו שומרים לנתיב מלא+יצירת איבר מתאים לתמונה במסך
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null!;
            try
            {
                string path = (string)value;
                string fullPath = Path.Combine(Environment.CurrentDirectory, path);
                BitmapImage image = new BitmapImage();
                image.BeginInit();

                image.UriSource = new System.Uri(fullPath ?? throw new Exception("problem"));
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                return image;
            }
            catch(Exception )
            {
                return null!;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null!;
            //throw new NotImplementedException();
        }
    }
}
