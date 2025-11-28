using Microsoft.Xaml.Behaviors;
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
    public class ContentAnimationBehavior : Behavior<ContentControl>
    {
        public static readonly DependencyProperty AnimationTypeProperty =
            DependencyProperty.Register(nameof(AnimationType), typeof(AnimationType),
            typeof(ContentAnimationBehavior), new PropertyMetadata(AnimationType.SlideFromRight));

        public AnimationType AnimationType
        {
            get => (AnimationType)GetValue(AnimationTypeProperty);
            set => SetValue(AnimationTypeProperty, value);
        }

        private object? _lastContent;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.ClipToBounds = true;
            _lastContent = AssociatedObject.Content;

            // 监听Content变化
            var descriptor = DependencyPropertyDescriptor.FromProperty(
                ContentControl.ContentProperty, typeof(ContentControl));
            descriptor?.AddValueChanged(AssociatedObject, OnContentChanged);
        }

        protected override void OnDetaching()
        {
            var descriptor = DependencyPropertyDescriptor.FromProperty(
                ContentControl.ContentProperty, typeof(ContentControl));
            descriptor?.RemoveValueChanged(AssociatedObject, OnContentChanged);
            base.OnDetaching();
        }

        private async void OnContentChanged(object? sender, EventArgs e)
        {
            if (AssociatedObject.Content == _lastContent) return;

            var newContent = AssociatedObject.Content as FrameworkElement;
            if (newContent == null) return;

            // 执行动画
            await AnimateContent(newContent);
            _lastContent = newContent;
        }

        private Task AnimateContent(FrameworkElement content)
        {
            var tcs = new TaskCompletionSource<bool>();

            switch (AnimationType)
            {
                case AnimationType.SlideFromRight:
                    AnimateSlideFromRight(content, tcs);
                    break;
                case AnimationType.FadeIn:
                    AnimateFadeIn(content, tcs);
                    break;
                    // 其他动画类型...
            }

            return tcs.Task;
        }

        private void AnimateSlideFromRight(FrameworkElement content, TaskCompletionSource<bool> tcs)
        {
            var transform = new TranslateTransform(300, 0);
            content.RenderTransform = transform;
            content.Opacity = 0;

            var storyboard = new Storyboard();

            var slideAnimation = new DoubleAnimation(300, 0, TimeSpan.FromMilliseconds(500))
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(slideAnimation, content);
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("RenderTransform.X"));

            var fadeAnimation = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(400));
            Storyboard.SetTarget(fadeAnimation, content);
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("Opacity"));

            storyboard.Children.Add(slideAnimation);
            storyboard.Children.Add(fadeAnimation);

            storyboard.Completed += (s, e) =>
            {
                content.RenderTransform = Transform.Identity;
                tcs.SetResult(true);
            };

            storyboard.Begin();
        }

        private void AnimateFadeIn(FrameworkElement content, TaskCompletionSource<bool> tcs)
        {
            content.Opacity = 0;
            var fadeAnimation = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(500));
            fadeAnimation.Completed += (s, e) => tcs.SetResult(true);
            content.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
        }
    }

    public enum AnimationType
    {
        SlideFromRight,
        SlideFromLeft,
        FadeIn,
        ScaleIn
    }
}
