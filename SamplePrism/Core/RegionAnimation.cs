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
                //1，启用硬件加速缓存以提高动画性能
                var cache = new BitmapCache
                {
                    EnableClearType = false,
                    RenderAtScale = 1.0
                };
                element.CacheMode = cache;

                //2，设置渲染选项以优化动画效果
                RenderOptions.SetBitmapScalingMode(element, BitmapScalingMode.LowQuality);
                //3，设置边缘模式以减少锯齿
                RenderOptions.SetEdgeMode(element, EdgeMode.Aliased);
                //4，创建缓动效果
                var easing = new CubicEase { EasingMode = EasingMode.EaseOut };


                var transform = new TranslateTransform(300, 0);
                var duration = TimeSpan.FromMilliseconds(250);
                element.RenderTransform = transform;
                element.Opacity = 0;

                var slideAnim = new DoubleAnimation
                {
                    From = 300,
                    To = 0,
                    Duration = duration,
                    EasingFunction = easing
                };

                var fadeAnim = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = duration,
                    EasingFunction = easing
                };
                slideAnim.Completed += (s, e) =>
                {
                    element.RenderTransform = Transform.Identity;
                    element.CacheMode = null; // 清除缓存以释放资源
                    element.ClearValue(RenderOptions.BitmapScalingModeProperty);
                    element.ClearValue(RenderOptions.EdgeModeProperty);

                };
                transform.BeginAnimation(TranslateTransform.XProperty, slideAnim);
                element.BeginAnimation(UIElement.OpacityProperty, fadeAnim);
            }
        }
    }
}
