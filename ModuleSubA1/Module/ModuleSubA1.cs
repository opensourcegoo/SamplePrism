using ModuleSubA1.ViewModels;
using ModuleSubA1.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleSubA1.Module
{
    public class ModuleSubA1 : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SubViewA1, ModuleSubA1ViewModel>();
            ViewModelLocationProvider.Register<SubViewA1, ModuleSubA1ViewModel>();
        }
    }
}
