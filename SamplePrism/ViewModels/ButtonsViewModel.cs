using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.ViewModels
{
    public class ButtonsViewModel : BindableBase
    {
        #region Property

        #endregion

        #region Command
        public DelegateCommand DismissCommand { get; set; }
        #endregion

        #region Constructor
        public ButtonsViewModel()
        {
            DismissCommand = new DelegateCommand(ExecuteDismissCommand);
        }

        #endregion


        #region Method
        private void ExecuteDismissCommand()
        {
            
        }

        #endregion


    }
}
