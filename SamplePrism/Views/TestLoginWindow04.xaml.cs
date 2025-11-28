using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SamplePrism.Views
{
    /// <summary>
    /// TestLoginWindow04.xaml 的交互逻辑
    /// </summary>
    public partial class TestLoginWindow04 : Window
    {
        public TestLoginWindow04()
        {
            InitializeComponent();

            // 设置窗口拖拽
            this.MouseDown += (sender, e) =>
            {
                if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                    this.DragMove();
            };
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            // 验证输入
            if (string.IsNullOrWhiteSpace(UserNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                ShowValidationError("请输入用户名和密码");
                return;
            }

            // 开始加载动画
            await StartLoginAnimation();
        }

        private async Task StartLoginAnimation()
        {
            // 禁用登录按钮
            SignInButton.IsEnabled = false;
            SignInButton.Content = "";

            // 显示加载动画
            LoadingPanel.Visibility = Visibility.Visible;
            var loadingStoryboard = (Storyboard)FindResource("LoadingAnimation");
            loadingStoryboard.Begin();

            // 模拟登录过程
            await Task.Delay(2000);

            // 成功动画
            await ShowSuccessAnimation();
        }

        private async Task ShowSuccessAnimation()
        {
            // 停止加载动画
            LoadingPanel.Visibility = Visibility.Collapsed;
            var loadingStoryboard = (Storyboard)FindResource("LoadingAnimation");
            loadingStoryboard.Stop();

            // 显示成功状态
            SignInButton.Content = "✓ SUCCESS";
            SignInButton.Background = new System.Windows.Media.SolidColorBrush(
                System.Windows.Media.Color.FromRgb(34, 197, 94));

            await Task.Delay(1000);

            // 淡出动画
            var fadeOutStoryboard = new Storyboard();
            var fadeOutAnimation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(500));
            Storyboard.SetTarget(fadeOutAnimation, this);
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath("Opacity"));
            fadeOutStoryboard.Children.Add(fadeOutAnimation);

            fadeOutStoryboard.Completed += (s, e) =>
            {
                // 这里可以导航到主界面
                MessageBox.Show("登录成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            };

            fadeOutStoryboard.Begin();
        }

        private void ShowValidationError(string message)
        {
            // 创建错误提示动画
            var shakeAnimation = new DoubleAnimationUsingKeyFrames();
            shakeAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))));
            shakeAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(-10, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(100))));
            shakeAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(10, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200))));
            shakeAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(-5, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300))));
            shakeAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(5, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(400))));
            shakeAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500))));

            var transform = new System.Windows.Media.TranslateTransform();
            LoginPanel.RenderTransform = transform;

            var storyboard = new Storyboard();
            Storyboard.SetTarget(shakeAnimation, transform);
            Storyboard.SetTargetProperty(shakeAnimation, new PropertyPath("X"));
            storyboard.Children.Add(shakeAnimation);
            storyboard.Begin();

            // 显示错误消息
            MessageBox.Show(message, "输入错误", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        // 窗口加载完成后的额外动画
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 为输入框添加浮动标签效果
            AddFloatingLabelEffect();
        }

        private void AddFloatingLabelEffect()
        {
            UserNameTextBox.GotFocus += (s, e) => AnimateFloatingLabel("User Name", true);
            UserNameTextBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrEmpty(UserNameTextBox.Text))
                    AnimateFloatingLabel("User Name", false);
            };

            PasswordBox.GotFocus += (s, e) => AnimateFloatingLabel("Password", true);
            PasswordBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrEmpty(PasswordBox.Password))
                    AnimateFloatingLabel("Password", false);
            };
        }

        private void AnimateFloatingLabel(string labelText, bool isFloating)
        {
            // 这里可以添加更复杂的浮动标签动画逻辑
            // 由于XAML中已经有基本的焦点动画，这里主要用于扩展
        }

        // 关闭按钮事件（如果需要）
        protected override void OnMouseRightButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            var contextMenu = new System.Windows.Controls.ContextMenu();
            var closeItem = new System.Windows.Controls.MenuItem() { Header = "关闭" };
            closeItem.Click += (s, args) => this.Close();
            contextMenu.Items.Add(closeItem);
            contextMenu.IsOpen = true;
        }
    }

    // 自定义动画帮助类
    public static class AnimationHelper
    {
        public static DoubleAnimation CreateSlideAnimation(double from, double to, TimeSpan duration)
        {
            var animation = new DoubleAnimation(from, to, duration);
            animation.EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut };
            return animation;
        }

        public static DoubleAnimation CreateFadeAnimation(double from, double to, TimeSpan duration)
        {
            return new DoubleAnimation(from, to, duration);
        }

        public static ColorAnimation CreateColorAnimation(System.Windows.Media.Color from,
                                                        System.Windows.Media.Color to, TimeSpan duration)
        {
            return new ColorAnimation(from, to, duration);
        }
    }
}
