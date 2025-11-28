using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.ViewModels
{
    public class TestLoginWindow06ViewModel : BindableBase
    {
        public class LanguageItem
        {
            public string Display { get; set; }
            public string Code { get; set; }
        }

        public ObservableCollection<LanguageItem> Languages { get; } = new()
        {
            new LanguageItem { Display = "中文", Code = "zh-CN" },
            new LanguageItem { Display = "English", Code = "en-US" }
        };

        public string Title => _currentLanguage?.Code == "zh-CN"
           ? "神经重症多模态监护系统"
           : "Neurocritical Care Multimodal Monitoring System";

        public string SubTitle => _currentLanguage?.Code == "zh-CN"
            ? "智能医疗监控平台"
            : "Intelligent Medical Monitoring Platform";


        public string UserName => _currentLanguage?.Code == "zh-CN"
            ? "用户名"
            : "UserName";

        public string Password => _currentLanguage?.Code == "zh-CN"
            ? "密码"
            : "Password";


        public string ForgetPassword => _currentLanguage?.Code == "zh-CN"
            ? "忘记密码？"
            : "FORGOT PASSWORD?";
        //public string Login => _currentLanguage?.Code == "zh-CN"
        //            ? "登录"
        //            : "Login";

        public string NOAccount => _currentLanguage?.Code == "zh-CN"
            ? "没有账户？"
            : "Don't have an account?";


        public string SignIn => _currentLanguage?.Code == "zh-CN"
            ? "登录"
            : "SIGN IN";

        public string SignUp => _currentLanguage?.Code == "zh-CN"
            ? "注册"
            : "Sign Up";

        public string CompanyInfo => _currentLanguage?.Code == "zh-CN"
            ? "© 2025 北京北科睿新医疗科技有限责任公司"
            : "© 2025 Bkrx Technology Co., Ltd";

        public string MoreAbout => _currentLanguage?.Code == "zh-CN"
            ? "了解更多"
            : "LEARN More";


        private LanguageItem _currentLanguage;
        public LanguageItem CurrentLanguage
        {
            get => _currentLanguage;
            set
            {
                if (_currentLanguage != value)
                {
                    _currentLanguage = value;
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(value.Code);

                    Thread.CurrentThread.CurrentCulture = new CultureInfo(value.Code);
                    RaisePropertyChanged(nameof(CurrentLanguage));
                    RaisePropertyChanged(nameof(Title));
                    RaisePropertyChanged(nameof(SubTitle));
                    RaisePropertyChanged(nameof(UserName));
                    RaisePropertyChanged(nameof(Password));
                    RaisePropertyChanged(nameof(ForgetPassword));
                    //RaisePropertyChanged(nameof(Login));
                    RaisePropertyChanged(nameof(NOAccount));
                    RaisePropertyChanged(nameof(SignIn));
                    RaisePropertyChanged(nameof(SubTitle));
                    RaisePropertyChanged(nameof(CompanyInfo));
                    RaisePropertyChanged(nameof(MoreAbout));
                    RaisePropertyChanged(nameof(SignUp));
                    

                }
            }
        }
        public TestLoginWindow06ViewModel()
        {
            CurrentLanguage = Languages[0]; // 默认中文
        }
    }
}
