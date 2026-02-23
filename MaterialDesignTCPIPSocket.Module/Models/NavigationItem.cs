using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignTCPIPSocket.Module.Models
{
    public class NavigationItem : BindableBase
    {
        #region Property
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _viewName;
        public string ViewName
        {
            get { return _viewName; }
            set { SetProperty(ref _viewName, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }


        #endregion
        public NavigationItem(string title, string viewName, bool isSelected)
        {
            Title = title;
            ViewName = viewName;
            IsSelected = isSelected;
        }


    }
}
