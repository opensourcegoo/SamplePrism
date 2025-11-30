using DryIoc;
using MaterialDesignThemes.Wpf.Transitions;
using ModuleA.Views;
using SamplePrism.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SamplePrism.ViewModels
{
    public class MainViewModel : BindableBase, INavigationAware
    {
        #region Fileds
        IRegionManager _regionManager;
        DefaultView? _defaultView = null;
        ViewA? _viewA = null;
        IContainerExtension _containerExtension;
        IDialogService _dialogService;
        #endregion

        #region property
        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand<string> LoadCommand { get; set; }

        public DelegateCommand DialogSeviceCommand { get; set; }
        #endregion


        public MainViewModel(IRegionManager regionManager, IContainerExtension containerExtension, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _containerExtension = containerExtension;
            _dialogService = dialogService;
            NavigateCommand = new DelegateCommand<string>(NavigateService);
            LoadCommand = new DelegateCommand<string>(LoadDefault);
            DialogSeviceCommand = new DelegateCommand(ShowDialogSevice);
        }


        private void ShowDialogSevice()
        {
            #region 构建参数
            var parameter = new DialogParameters();
            parameter.Add("bian", 21);
            _dialogService.ShowDialog("HelloDiaglogServiceView", parameter, DialogChoseCallBack);
            //_dialogService.shw
            #endregion
        }

        private void DialogChoseCallBack()
        {

        }

        private void LoadDefault(string defaultParam)
        {
            #region 1，路径指向
            //_regionManager.RequestNavigate("MainRegion", "DefaultView");
            #endregion

            #region 2，拿到View和Region再Add (可添加多个view) 使用Activate和Deactivate来激活或关闭区域中的已添加的Views
            _defaultView = _containerExtension.Resolve<DefaultView>();
            _viewA = _containerExtension.Resolve<ViewA>();
            IRegion mainRegion = _regionManager.Regions["MainViewRegion"];
            if (mainRegion != null)
            {
                mainRegion.Add(_defaultView);
                mainRegion.Add(_viewA);
            }
            mainRegion.Activate(_defaultView);
            #endregion

        }

        //NavigationParameters

        private void NavigateService(string navPath)
        {
            var param = new NavigationParameters();
            switch (navPath)
            {
                case "ViewA":
                    _regionManager.RequestNavigate("MainViewRegion", "ViewA", param);
                    break;
                case "ViewB":
                    _regionManager.RequestNavigate("MainViewRegion", "ViewB", param);
                    break;
                default:
                    break;
            }

            
            //Dispatcher.InvokeAsync(async () =>
            //{
            //    await Task.Delay(1);
            //    MainTransitioner.Content = null;
            //    MainTransitioner.Content = MainTransitioner.Content; // 强刷一下就又滑了
            //});
            //Application.Current.Dispatcher.InvokeAsync(async () =>
            //{
            //    await Task.Delay(1); // 等待 Prism 把新 View 塞进去
            //                         // 找到 MainWindow 里的 TransitioningContent，强行触发一次内容变化
            //    var d = Application.Current.MainWindow.FindName("MainTransitioner");
            //    if (Application.Current.MainWindow.FindName("MainTransitioner") is TransitioningContent tc)
            //    {
            //        var temp = tc.Content;
            //        tc.Content = null;
            //        tc.Content = temp;
            //    }
            //});
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
    }
}
