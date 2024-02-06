using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App1
{
  public class UniformWidthPanel : Panel
  {
    public UniformWidthPanel()
    {
    }

    public static double GetScale(DependencyObject obj)
    {
      return (double)obj.GetValue(ScaleProperty);
    }

    public static void SetScale(DependencyObject obj, double value)
    {
      obj.SetValue(ScaleProperty, value);
    }

    // Using a DependencyProperty as the backing store for Scale.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ScaleProperty =
        DependencyProperty.RegisterAttached("Scale", typeof(double), typeof(UniformWidthPanel), new PropertyMetadata(1.0));



    protected override Size MeasureOverride(Size availableSize)
    {
      Size newSize = new Size();
      if (double.IsInfinity(availableSize.Width) == false)
        newSize.Width = availableSize.Width;

      if (double.IsInfinity(availableSize.Height) == false)
        newSize.Height = availableSize.Height;

      double NumChildren = this.Children.Count;

      if (NumChildren > 0)
      {
        double totalScale = 0.0;
        for (int i = 0; i < NumChildren; i++)
        {
          var child = this.Children[i];
          totalScale += UniformWidthPanel.GetScale(child);
        }

        //Size childSize = availableSize;
        //childSize.Width /= NumChildren;

        for (int i = 0; i < NumChildren; i++)
        {
          var Child = this.Children[i];

          Size scaledChildSize = availableSize;
          var childScale = UniformWidthPanel.GetScale(Child);
          scaledChildSize.Width *= childScale / totalScale; 

          Child.Measure(scaledChildSize);
          if (Child.DesiredSize.Height > newSize.Height)
            newSize.Height = Child.DesiredSize.Height;
        }
      }
      return newSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      double numChildren = this.Children.Count;
      if (numChildren > 0)
      {
        double totalScale = 0.0;
        for (int i = 0; i < numChildren; i++)
        {
          var child = this.Children[i];
          totalScale += UniformWidthPanel.GetScale(child);
        }

        double AccumulatedScale = 0.0;

        var NewLeft = 0;
        for (int i = 0; i < numChildren; i++)
        {
          var child = this.Children[i];
          AccumulatedScale += UniformWidthPanel.GetScale(child);

          //var newRight = (int)Math.Round(finalSize.Width / numChildren * (i + 1));

          var newRight = (int)Math.Round(finalSize.Width * AccumulatedScale / totalScale);
          var newWidth = newRight - NewLeft;
          Rect childRect = new Rect(NewLeft, 0, newWidth, finalSize.Height);
          child.Arrange(childRect);
          NewLeft += newWidth;
        }
        return finalSize;
      }
      else
        return new Size();
    }
  }
}
