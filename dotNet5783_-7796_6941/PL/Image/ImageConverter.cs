﻿using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PL.Image;

class ImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            if (!File.Exists((string)value))
                throw new Exception("");

            BitmapImage b = new BitmapImage(new Uri((string)value, UriKind.RelativeOrAbsolute));
            Console.WriteLine(b.DpiX);
            return b;
        }
        catch (Exception)
        {
            return new BitmapImage(new Uri(@"images\passport\empty_image.gif", UriKind.RelativeOrAbsolute));
        }
    }



    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            return ((BitmapImage)value).UriSource.AbsolutePath;
        }
        catch
        {
            return @"images\passport\empty_image.gif";
        }
    }


}