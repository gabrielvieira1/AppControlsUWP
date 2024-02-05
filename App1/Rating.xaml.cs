using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace App1
{
  public sealed partial class Rating : UserControl
  {
    public Rating()
    {
      this.InitializeComponent();
    }

    public int Value
    {
      get { return (int)GetValue(ValueProperty); }
      set { SetValue(ValueProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(int), typeof(Rating), new PropertyMetadata(0));
  }
  public class GreaterOrEqualToIntToVisibilityConverter : IValueConverter
  {

    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return ((int)value >= CompareValue) ? Visibility.Visible : Visibility.Collapsed;
    }
    public int CompareValue { get; set; }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotImplementedException();
    }
  }
}
