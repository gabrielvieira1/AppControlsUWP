using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace App1
{
  public sealed class ClearableContentControl : ContentControl
  {
    public ClearableContentControl()
    {
      this.DefaultStyleKey = typeof(ClearableContentControl);
    }

    protected override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      _ClearButton = this.GetTemplateChild("PART_ClearButton") as Button;
      _ClearButton.Click += (s, e) => { this.Content = null; };
      
    }

    Button _ClearButton;
  }
}
