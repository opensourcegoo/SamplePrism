using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SamplePrism.Core
{
    public static class RegionAnimation
    {
        public static readonly DependencyProperty EnableProperty =
            DependencyProperty.RegisterAttached("Enable", typeof(bool), typeof(RegionAnimation),
                new PropertyMetadata(false, OnEnableChanged));

        public static void SetEnable(DependencyObject element, bool value)
            => element.SetValue(EnableProperty, value);

        public static bool GetEnable(DependencyObject element)
            => (bool)element.GetValue(EnableProperty);

        private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContentControl control && (bool)e.NewValue)
            {
                control.ClipToBounds = true;
                var descriptor = DependencyPropertyDescriptor.FromProperty(ContentControl.ContentProperty, typeof(ContentControl));
                descriptor.AddValueChanged(control, (s, args) => AnimateContent(control));
            }
        }

        private static void AnimateContent(ContentControl control)
        {
            if (control.Content is FrameworkElement element)
            {
                var transform = new TranslateTransform(300, 0);
                element.RenderTransform = transform;
                element.Opacity = 0;
                var slideAnim = new DoubleAnimation(300, 0, TimeSpan.FromMilliseconds(300));
                var fadeAnim = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(500));
                slideAnim.Completed += (s, e) => element.RenderTransform = Transform.Identity;
                transform.BeginAnimation(TranslateTransform.XProperty, slideAnim);
                element.BeginAnimation(UIElement.OpacityProperty, fadeAnim);
            }
        }
    }
}
