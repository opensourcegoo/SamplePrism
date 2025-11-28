using ModuleSubA2.ViewModels;
using ModuleSubA2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleSubA2.Module
{
    public class ModuleSubA2 : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SubViewA2, ModuleSubA2ViewModel>();
            ViewModelLocationProvider.Register<SubViewA2, ModuleSubA2ViewModel>();
        }
    }
}
