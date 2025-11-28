using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SamplePrism.ViewModels
{


    public class HomeViewModel : BindableBase
    {
        #region 管用
        private ULMode _selectedULMode;
        private DLMode _selectedDLMode;
        private bool _isRollingBack;


        public record LinkRule(EA1 eA1, EA2 eA2, List<EA3> eA3List);
        List<LinkRule> rules = new List<LinkRule>
        {
            new(EA1.N4, EA2.N1, new() { EA3.Zero, EA3.One, EA3.Two, EA3.Three }),
            new(EA1.N4, EA2.N2, new() { EA3.One, EA3.Two, EA3.Three }),
            new(EA1.N4, EA2.N4, new() { EA3.Three }),
            new(EA1.N2, EA2.N1, new() { EA3.Zero, EA3.One }),
            new(EA1.N2, EA2.N2, new() { EA3.One }),
            new(EA1.N1, EA2.N1, new() { EA3.One }),
        };

        public List<EA1> AvaiableA1Lsit { get; } = Enum.GetValues(typeof(EA1)).Cast<EA1>().ToList();

        private List<EA2> _avaiableA2List;
        public List<EA2> AvaiableA2Lsit
        {
            get { return _avaiableA2List; }
            set { SetProperty(ref _avaiableA2List, value); }
        }

        private List<EA3> _avaiableA3List;
        public List<EA3> AvaiableA3Lsit
        {
            get { return _avaiableA3List; }
            set { SetProperty(ref _avaiableA3List, value); }
        }

        private EA1 _selectedEA1;
        public EA1 SelectedEA1
        {
            get { return _selectedEA1; }
            set
            {
                if (_selectedEA1 != value)
                {
                    //_selectedEA1 = value;
                    SetProperty(ref _selectedEA1, value);
                    RaisePropertyChanged(nameof(SelectedEA1));
                    UpdateEA2();
                }
            }
        }

        private EA2 _selectedEA2;
        public EA2 SelectedEA2
        {
            get { return _selectedEA2; }
            set 
            { 
                if (_selectedEA2 != value)
                {
                    //_selectedEA2 = value;
                    SetProperty(ref _selectedEA2, value);
                    RaisePropertyChanged(nameof(SelectedEA2));
                    UpdateEA3();
                }
            }
        }

        private EA3 _selectedEA3;
        public EA3 SelectedEA3
        {
            get { return _selectedEA3; }
            set { SetProperty(ref _selectedEA3, value); }
        }


        private void UpdateEA2()
        {
            AvaiableA2Lsit = rules.Where(r => r.eA1 == SelectedEA1).Select(r => r.eA2).Distinct().ToList();
            SelectedEA2 = AvaiableA2Lsit.FirstOrDefault();
            UpdateEA3();
        }

        private void UpdateEA3()
        {
            var tmp = rules.Where(r => r.eA1 == SelectedEA1).Select(r => r.eA3List).Distinct().ToList();
            AvaiableA3Lsit = rules.Where(r => r.eA1 == SelectedEA1 && r.eA2 == SelectedEA2).SelectMany(r => r.eA3List).Distinct().ToList();
            SelectedEA3 = AvaiableA3Lsit.FirstOrDefault();
        }


        public HomeViewModel()
        {
            SelectedEA1 = EA1.N4;
            UlModeList = Enum.GetValues(typeof(ULMode)).Cast<ULMode>().ToList();
            DlModeList = Enum.GetValues(typeof(DLMode)).Cast<DLMode>().ToList();
            SelectedULMode = UlModeList[8];
            SelectedDLMode = DlModeList[8];
        }


        
        public List<ULMode> UlModeList { get; }
        public List<DLMode> DlModeList { get; }

        public ULMode SelectedULMode
        {
            get => _selectedULMode;
            set
            {
                if (_isRollingBack) return;
                var old = _selectedULMode;
                SetProperty(ref _selectedULMode, value);

                if ((int)value + (int)_selectedDLMode > 13)
                {
                    //MessageBox.Show("UL + DL 不能超过 13！");

                    // 延迟回退到上一帧执行
                    _isRollingBack = true;
                    Application.Current.Dispatcher.BeginInvoke(
                        DispatcherPriority.Background,
                        new Action(() =>
                        {
                            SetProperty(ref _selectedULMode, old);
                            RaisePropertyChanged(nameof(SelectedULMode)); // 强制刷新 UI
                            _isRollingBack = false;
                        }));
                }
            }
        }

        public DLMode SelectedDLMode
        {
            get => _selectedDLMode;
            set
            {
                if (_isRollingBack) return;
                var old = _selectedDLMode;
                SetProperty(ref _selectedDLMode, value);

                if ((int)_selectedULMode + (int)value > 13)
                {
                    //MessageBox.Show("UL + DL 不能超过 13！");

                    _isRollingBack = true;
                    Application.Current.Dispatcher.BeginInvoke(
                        DispatcherPriority.Background,
                        new Action(() =>
                        {
                            SetProperty(ref _selectedDLMode, old);
                            RaisePropertyChanged(nameof(SelectedDLMode));
                            _isRollingBack = false;
                        }));
                }
            }
        }
        #endregion


    }

    public enum EA1 { N1, N2, N4 }
    public enum EA2 { N1, N2, N4 }
    public enum EA3
    {
        [Description("0")]
        Zero = 0,
        [Description("1")]
        One,
        [Description("2")]
        Two,
        [Description("3")]
        Three
    }

    // 枚举定义示例   
    public enum ULMode
    {
        Mode0 = 0,
        Mode1 = 1,
        Mode2 = 2,
        Mode3 = 3,
        Mode4 = 4,
        Mode5 = 5,
        Mode6 = 6,
        Mode7 = 7,
        Mode8 = 8,
        Mode9 = 9,
        Mode10 = 10,
        Mode11 = 11,
        Mode12 = 12,
        Mode13 = 13
    }

    public enum DLMode
    {
        Mode0 = 0,
        Mode1 = 1,
        Mode2 = 2,
        Mode3 = 3,
        Mode4 = 4,
        Mode5 = 5,
        Mode6 = 6,
        Mode7 = 7,
        Mode8 = 8,
        Mode9 = 9,
        Mode10 = 10,
        Mode11 = 11,
        Mode12 = 12,
        Mode13 = 13
    }

}
