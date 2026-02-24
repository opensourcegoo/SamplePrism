using MaterialDesignTCPIPSocket.Module.ViewModels;
using MaterialDesignTCPIPSocket.Module.Views;

namespace MaterialDesignTCPIPSocket.Module
{
    public class MaterialDesignTCPIPSocketModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainViewRegion", typeof(MaterialDesignTCPIPSocketView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MaterialDesignTCPIPSocketView, MaterialDesignTCPIPSocketViewModel>();
            containerRegistry.RegisterForNavigation<TestGoBackView, TestGoBackViewModel>();
            containerRegistry.RegisterForNavigation<TestPuschView, TestPuschViewModel>();
            containerRegistry.RegisterForNavigation<TestPuschView>();// 视图注册到容器中，导航时会自动创建实例,无需再注册一次ViewModel
        }
    }
}
