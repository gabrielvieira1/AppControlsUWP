using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App1
{
  public class TextBoxUtils
  {
    public static string GetValidationRegEx(DependencyObject obj)
    {
      return (string)obj.GetValue(ValidationRegExProperty);
    }

    public static void SetValidationRegEx(DependencyObject obj, string value)
    {
      obj.SetValue(ValidationRegExProperty, value);
    }

    public static readonly DependencyProperty ValidationRegExProperty =
    DependencyProperty.RegisterAttached("ValidationRegEx", typeof(string), typeof(TextBoxUtils), new PropertyMetadata(null, (obj, args) =>
    {
      var tb = obj as TextBox;
      if (tb != null)
      {
        if (args.NewValue != null)
        {
          if (args.OldValue == null)
            tb.TextChanged += Tb_TextChanged;
        }
        else
        {
          var ecn = TextBoxUtils.GetErrorControlName(tb);
          if (ecn != null)
          {
            var uie = (UIElement)tb.FindName(ecn);
            if (uie != null)
              uie.Visibility = Visibility.Collapsed;
          }
          tb.TextChanged -= Tb_TextChanged;
        }
      }
    }));

    // Codigo Errado

    //public static readonly DependencyProperty ValidationRegExProperty =
    //    DependencyProperty.RegisterAttached("ValidationRegEx", typeof(string), typeof(TextBoxUtils), new PropertyMetadata(null, (obj, args) =>
    //    {
    //      var tb = obj as TextBox;
    //      if (tb != null)
    //        return;
    //      if (args.NewValue != null)
    //      {
    //        if (args.OldValue == null)
    //          tb.TextChanged += Tb_TextChanged;
    //      }
    //      else
    //      {
    //        var ecn = TextBoxUtils.GetErrorControlName(tb);
    //        if(ecn != null)
    //        {
    //          var uie = (UIElement)tb.FindName(ecn);
    //          if (uie != null)
    //            uie.Visibility = Visibility.Collapsed;
    //        }
    //        tb.TextChanged -= Tb_TextChanged;
    //      }
    //    }));

    private static void Tb_TextChanged(object sender, TextChangedEventArgs e)
    {
      ValidateTextBox(sender as TextBox);
    }

    private static void ValidateTextBox(TextBox tb)
    {
      var exp = TextBoxUtils.GetValidationRegEx(tb);
      var ecn = TextBoxUtils.GetErrorControlName(tb);
      if (ecn == null)
        return;
      var uie = (UIElement)tb.FindName(ecn);
      if (uie == null)
        return;

      bool IsValid = true;
      if (string.IsNullOrEmpty(exp) == false && tb.Text != null)
      {
        var re = new Regex(exp);
        IsValid = re.IsMatch(tb.Text);
      }
      uie.Visibility = IsValid ? Visibility.Collapsed : Visibility.Visible;
    }

    public static string GetErrorControlName(DependencyObject obj)
    {
      return (string)obj.GetValue(ErrorControlNameProperty);
    }

    public static void SetErrorControlName(DependencyObject obj, string value)
    {
      obj.SetValue(ErrorControlNameProperty, value);
    }

    // Using a DependencyProperty as the backing store for ErrorControlName.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ErrorControlNameProperty =
        DependencyProperty.RegisterAttached("ErrorControlName", typeof(string), typeof(TextBoxUtils), new PropertyMetadata(null, (obj, args) =>
        {
          var tb = obj as TextBox;
          if (tb == null)
            return;
          if (args.OldValue != null)
          {
            var uie = (UIElement)tb.FindName(args.OldValue as string);
            if (uie != null)
              uie.Visibility = Visibility.Collapsed;
          }
          ValidateTextBox(tb);
        }));

  }
}
