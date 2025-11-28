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
    /// TestLoginWindow06.xaml 的交互逻辑
    /// </summary>
    public partial class TestLoginWindow06 : Window
    {
        public TestLoginWindow06()
        {
            InitializeComponent();


            this.MouseDown += (sender, e) =>
            {
                if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                    this.DragMove();
            };
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(UserNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                ShowValidationError("请输入用户名和密码");
                return;
            }


            await StartLoginAnimation();
        }

        private async Task StartLoginAnimation()
        {

            SignInButton.IsEnabled = false;
            SignInButton.Content = "";


            LoadingPanel.Visibility = Visibility.Visible;
            var loadingStoryboard = (Storyboard)FindResource("LoadingAnimation");
            loadingStoryboard.Begin();

            await Task.Delay(2000);

            await ShowSuccessAnimation();
        }

        private async Task ShowSuccessAnimation()
        {
            LoadingPanel.Visibility = Visibility.Collapsed;
            var loadingStoryboard = (Storyboard)FindResource("LoadingAnimation");
            loadingStoryboard.Stop();

            // 显示成功状态
            SignInButton.Content = "✓ 成功";
            //SignInButton.Content = "✓ SUCCESS";
            SignInButton.Background = new System.Windows.Media.SolidColorBrush(
                System.Windows.Media.Color.FromRgb(34, 197, 94));

            await Task.Delay(800);


            LoginFormContainer.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            var actualHeight = LoginFormContainer.DesiredSize.Height;


            if (double.IsNaN(actualHeight) || actualHeight <= 0)
            {
                actualHeight = LoginFormContainer.ActualHeight;
            }


            LoginFormContainer.Height = actualHeight;


            var successStoryboard = (Storyboard)FindResource("LoginSuccessAnimation");
            successStoryboard.Completed += OnLoginSuccessAnimationCompleted;
            successStoryboard.Begin();
        }

        private void OnLoginSuccessAnimationCompleted(object sender, EventArgs e)
        {

            Task.Delay(2000).ContinueWith(t =>
            {
                Dispatcher.Invoke(() =>
                {

                    var finalFadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(500));
                    finalFadeOut.Completed += (s, args) =>
                    {
                        MessageBox.Show("即将进入主界面...", "登录成功", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    };
                    this.BeginAnimation(OpacityProperty, finalFadeOut);
                });
            });
        }

        private void ShowValidationError(string message)
        {

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


            MessageBox.Show(message, "输入错误", MessageBoxButton.OK, MessageBoxImage.Warning);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

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

        }


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
}
