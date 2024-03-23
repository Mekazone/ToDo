using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DoToo.Converters
{
    //converts boolean values to colors
    //afterwards, add a global resource in App.xaml and add the namespace
    //If you don't want it globally, you can add a ContentPage.Resource to the View page only
    //Finally, bind it to the control of interest in the view
    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            return (bool)value ? (Color)Application.Current.Resources["CompletedColor"] :
                                 (Color)Application.Current.Resources["ActiveColor"];
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
