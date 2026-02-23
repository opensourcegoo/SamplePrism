using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignButtons.Module.ViewModels
{
    public class TestGoBackViewModel : BindableBase
    {
        #region Commands
        public DelegateCommand GoBackCommand { get; set; }
        #endregion
        public TestGoBackViewModel()
        {

        }
    }
}
