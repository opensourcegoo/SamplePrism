using DryIoc.FastExpressionCompiler.LightExpression;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MaterialDesignButtons.Module.ViewModels
{
    public class MaterialDesignButtonsViewModel : BindableBase
    {
        #region Fileds
        bool _isDismisseRequired = false;


        #endregion

        #region Property
        private int _badgeClickMeCount;
        public int BadgeClickMeCount
        {
            get => _badgeClickMeCount;
            set => SetProperty(ref _badgeClickMeCount, value);
        }

        private int _incrementOrClickMeCount;
        public int IncrementOrClickMeCount
        {
            get => _incrementOrClickMeCount;
            set => SetProperty(ref _incrementOrClickMeCount, value);
        }

        private string _restartCountDownText;
        public string RestartCountDownText
        {
            get => _restartCountDownText;
            set => SetProperty(ref _restartCountDownText, value);
        }

        private bool _isShowDismissButton = true;
        public bool IsShowDismissButton
        {
            get => _isShowDismissButton;
            set => SetProperty(ref _isShowDismissButton, value);
        }

        private double _dismissProgress = 0.0;
        public double DismissProgress
        {
            get => _dismissProgress;
            set => SetProperty(ref _dismissProgress, value);
        }

        private bool _isSaveComplete = false;
        public bool IsSaveComplete
        {
            get => _isSaveComplete;
            set => SetProperty(ref _isSaveComplete, value);
        }

        private bool _isSaving = false;
        public bool IsSaving
        {
            get => _isSaving;
            set => SetProperty(ref _isSaving, value);
        }

        private OptionType _selectedOption = OptionType.A4;
        public OptionType SelectedOption
        {
            get => _selectedOption;
            set => SetProperty(ref _selectedOption, value);
        }

        #endregion

        #region Command
        public DelegateCommand ClickMeCommand { get; set; }

        public DelegateCommand IncrementOrClickMeCommand { get; set; }

        public DelegateCommand DismissCommand { get; set; }

        public DelegateCommand SaveCommand { get; set; }
        #endregion

        #region Constructor
        public MaterialDesignButtonsViewModel()
        {
            ClickMeCommand = new DelegateCommand(OnClickMe);
            IncrementOrClickMeCommand = new DelegateCommand(OnIncrementOrClickMe);
            DismissCommand = new DelegateCommand(ExecuteDismissCommand);
            SaveCommand = new DelegateCommand(ExecuteSaveCommand);
            BadgeClickMeCount = 0;
            IncrementOrClickMeCount = 0;
            SelectedOption = OptionType.A4;
        }

        private void ExecuteSaveCommand()
        {
            if (!IsSaveComplete)
            {
                IsSaveComplete = true;
            }
            else
            {
                IsSaveComplete = false;
                return;
            }
            IsSaving = true;
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

        public delegate void TestDelegateCommand(object sender, EventArgs e);
        public TestDelegateCommand TestDelegateCommandHandler = new TestDelegateCommand((s, e) =>
        {

        });

        public delegate bool TestBoolDelegateCommand(object sender, EventArgs e);
        public TestBoolDelegateCommand TestBoolDelegateCommandHandler = new TestBoolDelegateCommand((s, e) =>
        {
            return true;
        });

        private Stopwatch _dismissWatch = new();
        private Stopwatch _restartWatch = new();

        private const double DismissSeconds = 3;
        private const double RestartSeconds = 3;

        //DateTime _currentTime = DateTime.Now;
        private void ExecuteDismissCommand()
        {
            DateTime _currentTime = DateTime.Now;
            DateTime demoRestartCountdownComplete = DateTime.Now;

            //_isDismisseRequired = true;
            int i = 0;
            //long totalDuration = TimeSpan.FromMicroseconds(5000).Ticks;
            _ = new DispatcherTimer(TimeSpan.FromMilliseconds(100), DispatcherPriority.Normal, new EventHandler((s, e) =>
            {
                if (IsShowDismissButton)
                {
                    long totalDuration = _currentTime.AddSeconds(3).Ticks - _currentTime.Ticks;
                    //long totalDuration = TimeSpan.FromMilliseconds(5000).Ticks;
                    long currentDuration = DateTime.Now.Ticks - _currentTime.Ticks;
                    double autoCountdownPercentComplete = 100.0 / totalDuration * currentDuration;
                    DismissProgress = autoCountdownPercentComplete;

                    if (autoCountdownPercentComplete >= 100)
                    {

                        //demoRestartCountdownComplete.AddSeconds(33);
                        _isDismisseRequired = false;
                        IsShowDismissButton = false;
                    }
                    //RestartCountDownText = string.Format("{0}", i++);
                }
                else
                {
                    UpdateDemoRestartCountdownText(DateTime.Now.AddSeconds(15), out bool isComplete);
                    if (isComplete)
                    {
                        //autoStartingActionCountdownStart = DateTime.Now;
                        //IsShowDismissButton = true;
                    }
                }
                //Console.WriteLine("========{0}", i++);
            }), Dispatcher.CurrentDispatcher);
        }

        private void UpdateDemoRestartCountdownText(DateTime endTime, out bool isComplete)
        {
            var span = endTime - DateTime.Now;
            var seconds = Math.Round(span.TotalSeconds < 0 ? 0 : span.TotalSeconds);
            RestartCountDownText = "Demo in " + seconds;
            isComplete = seconds == 0;
        }

        #endregion

    }
}
