using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignButtons.Module
{
    public class MaterialDesignButtonsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainViewRegion", typeof(Views.MaterialDesignButtonsView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.MaterialDesignButtonsView, ViewModels.MaterialDesignButtonsViewModel>();
        }
    }
}
