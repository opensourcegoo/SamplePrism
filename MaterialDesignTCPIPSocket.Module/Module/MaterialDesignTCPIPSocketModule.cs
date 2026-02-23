
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
        }
    }

}
