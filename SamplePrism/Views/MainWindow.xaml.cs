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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SamplePrism.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 这三行搞定全局：最小化、最大化、关闭，任何 UserControl 里的按钮都能直接点
            //CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand,
            //    (sender, e) => Close()));

            //CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand,
            //    (sender, e) => WindowState = WindowState.Maximized));

            //CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand,
            //    (sender, e) => WindowState = WindowState.Minimized));

            //CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand,
            //    (sender, e) => WindowState = WindowState.Normal));
        }
    }
}
