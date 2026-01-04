using ModuleA.ViewModels;
using ModuleA.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA.Module
{
    //[ModuleDependency("ModuleSubA1")]
    //[ModuleDependency("ModuleSubA2")]
    public class ModuleA : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            #region 需要导航用字符串，先在RegisterTypes注入
            //regionManager.RegisterViewWithRegion("leftRegion", nameof(ModuleSubA1.Views.SubViewA1));
            //regionManager.RegisterViewWithRegion("rightRegion", nameof(ModuleSubA2.Views.SubViewA2)); 
            #endregion

            #region 无需导航使用type
            regionManager.RegisterViewWithRegion("leftRegion", typeof(ModuleSubA1.Views.SubViewA1));
            regionManager.RegisterViewWithRegion("rightRegion", typeof(ModuleSubA2.Views.SubViewA2));
            #endregion

            //regionManager.RegisterViewWithRegion("leftRegion", "SubViewA1");
            //regionManager.RegisterViewWithRegion("rightRegion", "SubViewA2");

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<ModuleSubA1.Views.SubViewA1, SubViewA1ViewModel>();
            //containerRegistry.RegisterForNavigation<ModuleSubA2.Views.SubViewA2, SubViewA2ViewModel>();
        }
    }
}
