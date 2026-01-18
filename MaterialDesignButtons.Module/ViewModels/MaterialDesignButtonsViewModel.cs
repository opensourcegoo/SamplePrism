using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignButtons.Module.ViewModels
{
    public class MaterialDesignButtonsViewModel : BindableBase
    {
        #region Property
        private int _badgeClickMeCount;
        public int BadgeClickMeCount
        {
            get { return _badgeClickMeCount; }
            set { SetProperty(ref _badgeClickMeCount, value); }
        }

        private int _incrementOrClickMeCount;
        public int IncrementOrClickMeCount
        {
            get { return _incrementOrClickMeCount; }
            set { SetProperty(ref _incrementOrClickMeCount, value);}
        }


        #endregion

        #region Command
        public DelegateCommand ClickMeCommand { get; set; }

        public DelegateCommand IncrementOrClickMeCommand { get; set; }
        #endregion

        #region Constructor
        public MaterialDesignButtonsViewModel()
        {
            ClickMeCommand = new DelegateCommand(OnClickMe);
            IncrementOrClickMeCommand = new DelegateCommand(OnIncrementOrClickMe);
            BadgeClickMeCount = 0;
            IncrementOrClickMeCount = 0;
        }
        #endregion

        #region Method
        private void OnClickMe()
        {
            BadgeClickMeCount++;
            
        }

        private void OnIncrementOrClickMe()
        {
            IncrementOrClickMeCount++;
        }
        #endregion

    }
}
