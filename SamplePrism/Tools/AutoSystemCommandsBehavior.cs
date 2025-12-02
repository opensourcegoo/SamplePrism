using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace SamplePrism.Tools
{
    public class AutoSystemCommandsBehavior
    {
        #region demo
        public static readonly DependencyProperty IsEnabledProperty =
             DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(AutoSystemCommandsBehavior),
                 new PropertyMetadata(false, OnIsEnabledChanged));

        public static bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);
        public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window && (bool)e.NewValue)
            {
                window.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand,
                    (s, args) => window.Close()));

                window.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand,
                    (s, args) => window.WindowState = WindowState.Minimized));

                window.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand,
                    (s, args) => window.WindowState = WindowState.Maximized));

                window.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand,
                    (s, args) => window.WindowState = WindowState.Normal));
            }
        }
        #endregion

        #region 新版本的附加属性的Snippet
        //        <?xml version = "1.0" encoding="utf-8"?>
        //<CodeSnippets xmlns = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet" >
        //  < CodeSnippet Format="1.0.0">
        //    <Header>
        //      <Title>Correct Dependency Property</Title>
        //      <Shortcut>propdp</Shortcut>
        //      <Author>You</Author>
        //    </Header>
        //    <Snippet>
        //      <Declarations>
        //        <Literal>
        //          <ID>type</ID>
        //          <ToolTip>Property type</ToolTip>
        //          <Default>int</Default>
        //        </Literal>
        //        <Literal>
        //          <ID>property</ID>
        //          <ToolTip>Property name</ToolTip>
        //          <Default>MyProperty</Default>
        //        </Literal>
        //        <Literal>
        //          <ID>default</ID>
        //          <ToolTip>Default value</ToolTip>
        //          <Default>0</Default>
        //        </Literal>
        //      </Declarations>
        //      <Code Language = "csharp" >
        //        < ! [CDATA[public static readonly DependencyProperty $property$Property =
        //    DependencyProperty.Register("$property$", typeof($type$), typeof($end$), new PropertyMetadata($default$));

        //public $type$ $property$
        //{
        //    get => ($type$)GetValue($property$Property);
        //        set => SetValue($property$Property, value);
        //    }$end$]]>
        //      </Code>
        //    </Snippet>
        //  </CodeSnippet>
        //</CodeSnippets>
        #endregion
    }
}
