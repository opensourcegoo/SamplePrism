using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignTCPIPSocket.Module.ViewModels
{
    public class TestGoBackViewModel : BindableBase, INavigationAware
    {
        #region Fileds
        IRegionNavigationJournal _journal;
        #endregion

        #region Properties
        public DelegateCommand GoBackCommand { get; set; }
        #endregion
        public TestGoBackViewModel()
        {
            GoBackCommand = new DelegateCommand(() =>
            {
                if (_journal.CanGoBack)
                {
                    _journal.GoBack();
                }
            });
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
            _journal = navigationContext.NavigationService.Journal;
        }
    }
}
