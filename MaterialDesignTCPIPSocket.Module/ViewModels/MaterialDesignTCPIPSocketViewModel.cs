using DryIoc;
using MaterialDesignTCPIPSocket.Module.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace MaterialDesignTCPIPSocket.Module.ViewModels
{
    public class MaterialDesignTCPIPSocketViewModel : BindableBase, INavigationAware
    {
        #region Fileds
        IRegionManager _regionManager;
        IRegionNavigationJournal _navigationJournal;
        private int _cachedSelectedCustomerId;

        #endregion

        #region Properties
        public int SelectedCustomerId { get; set; }

        private ObservableCollection<NavigationItem> _navigationItems;

        public ObservableCollection<NavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set { SetProperty(ref _navigationItems, value); }
        }

        #endregion

        #region Commands
        public DelegateCommand NaviDetailCommand { get; set; }

        public DelegateCommand<NavigationItem> NavigateCommand { get; set; }
        #endregion

        #region Constructor
        public MaterialDesignTCPIPSocketViewModel(IRegionManager regionManager, IRegionNavigationJournal regionNavigationJournal)
        {
            _regionManager = regionManager;
            _navigationJournal = regionNavigationJournal;
            SelectedCustomerId = 12;
            NaviDetailCommand = new DelegateCommand(() =>
            {
                //导航到DetailView，并且传递参数
                var parameters = new NavigationParameters();
                parameters.Add("SelectedId", 33);
                _regionManager.RequestNavigate("MainViewRegion", "SocketBaseView", parameters);
            });

            NavigateCommand = new DelegateCommand<NavigationItem>((item) =>
            {
                foreach (var navItem in NavigationItems)
                    navItem.IsSelected = false;

                item.IsSelected = true;
                _regionManager.RequestNavigate("SubContentRegion", item.ViewName);

            });
            NavigationItems = new ObservableCollection<NavigationItem>()
            {
                new NavigationItem("SocketBase","SocketBaseView",true),
                new NavigationItem("OrderInfo","TestPuschView",false),
                new NavigationItem("PayInfo","TestPdschView",false),
            };
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// OnNavigatedFrom的使用
        /// <1>一般用来停止当前页面的某些操作(停止Timer定时器)
        /// <2>取消异步 _cts?.Cancel()，并且在异步方法中捕获异常，避免页面被销毁后异步方法继续执行引发异常
        /// <3>解除事件订阅，避免内存泄漏_eventAggregator.GetEvent<DataChangedEvent>().Unsubscribe(OnDataChanged);
        /// <4>保存当前页面的状态等
        /// Prism的导航 ≠ 页面切换不是简单的页面切换，而是一个完整的导航过程，涉及到页面的创建、销毁、状态管理等多个方面。OnNavigatedFrom方法是这个过程中非常重要的一环，它提供了一个机会，让开发者在页面即将被导航离开时执行一些必要的操作，以确保应用程序的稳定性和性能。
        /// 本质上是 View Activation / Deactivation（激活/失活），OnNavigatedFrom 本质：Deactivate Hook（失活钩子）
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //保存当前页面的状态
            navigationContext.Parameters.Add("SelectedId", SelectedCustomerId);
            _cachedSelectedCustomerId = SelectedCustomerId;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _regionManager.RequestNavigate("SubContentRegion", "SocketBaseView");
            _navigationJournal = navigationContext.NavigationService.Journal;
        }
        #endregion
    }
}
