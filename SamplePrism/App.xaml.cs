using Microsoft.Win32;
using ModuleA.Module;
using ModuleA.Views;
using ModuleB.Views;
using Prism.Dialogs;
using Prism.Ioc;
using SamplePrism.Dialog;
using SamplePrism.ViewModels;
using SamplePrism.Views;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace SamplePrism
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            bool mutexIsnew;
            using (Mutex m = new Mutex(true, "SamplePrism", out mutexIsnew))
            {
                if (!mutexIsnew)
                {
                    Application.Current.Shutdown();
                }
            }
        }

        protected override Window CreateShell()
        {
            var shell = Container.Resolve<MainWindow>();
            return shell;
        }

        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //    // Shell 已创建，容器可用，区域一般也已注册
        //    var regionManager = Container.Resolve<IRegionManager>();
        //    regionManager.RequestNavigate("MainRegion", "MainView");
        //}

        protected override void Initialize()
        {
            base.Initialize();
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate("MainRegion", "MainView");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
            containerRegistry.RegisterForNavigation<ButtonsView>();
            containerRegistry.RegisterForNavigation<MainView>();
            //containerRegistry.RegisterForNavigation<MainWindow>();
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterForNavigation<DefaultView>();
            containerRegistry.RegisterDialogWindow<MessageDialogHostView>();//注册IDialogWindows类型的窗口
            containerRegistry.RegisterDialog<HelloDiaglogServiceView, HelloDiaglogServiceViewModel>();
            //containerRegistry.RegisterDialog<HelloDiaglogServiceView, HelloDiaglogServiceViewModel>("", "notifywindow");

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<ModuleA.Module.ModuleA>();
            moduleCatalog.AddModule<ModuleB.Module.ModuleB>();
            moduleCatalog.AddModule<ModuleSubA1.Module.ModuleSubA1>();
            moduleCatalog.AddModule<ModuleSubA2.Module.ModuleSubA2>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            //regionAdapterMappings.RegisterMapping(typeof(System.Windows.Controls.ContentControl),
            //regionAdapterMappings.RegisterMapping(typeof(System.Windows.Controls.ContentControl),
            //    new AdvancedAnimatedRegionAdapter(Container.Resolve<IRegionBehaviorFactory>(),
            //        AdvancedAnimatedRegionAdapter.AnimationType.SlideFromRight));
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<MainView, MainViewModel>();
            ViewModelLocationProvider.Register<ButtonsView,ButtonsViewModel>();
            ViewModelLocationProvider.Register<HomeView, HomeViewModel>();
            ViewModelLocationProvider.Register<DefaultView, DefaultViewModel>();
            ViewModelLocationProvider.Register<TestLoginWindow06, TestLoginWindow06ViewModel>();
            ViewModelLocationProvider.Register<HelloDiaglogServiceView, HelloDiaglogServiceViewModel>();
        }


        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);
        }

        //DIP和IOC是软件开发中的两个重要概念：
        //DIP - 依赖倒置原则(Dependency Inversion Principle)

        //是SOLID原则中的"D"
        //核心思想：高层模块不应该依赖于低层模块，两者都应该依赖于抽象
        //抽象不应该依赖于具体实现，具体实现应该依赖于抽象
        //目的是降低代码耦合度，提高可维护性和可测试性

        //IOC - 控制反转(Inversion of Control)

        //是一种设计原则和编程思想
        //传统方式：对象主动创建和管理其依赖
        //IOC方式：将对象创建和依赖管理的控制权交给外部容器
        //常见实现方式包括依赖注入(DI)、服务定位器等

        //两者关系：

        //DIP是理论原则，IOC是实现这个原则的具体方法
        //IOC容器帮助实现依赖倒置，通过注入抽象接口而不是具体类
        //都旨在创建松耦合、易测试、易维护的代码结构

        //从"高层→低层"变成"高层→抽象←低层"
        //让具体实现去适应抽象接口，而不是让抽象去迎合具体实现

        //你的理解方向是对的，DIP确实是"反其道而行"！
    }

}
