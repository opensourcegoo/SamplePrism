using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.ViewModels
{
    public class HelloDiaglogServiceViewModel : BindableBase, IDialogAware
    {
        public DialogCloseListener RequestClose { get; }
        public DelegateCommand<string> CloseDialogCommand { get; set; }
        public HelloDiaglogServiceViewModel()
        {
            CloseDialogCommand = new DelegateCommand<string>(Close);
            RequestClose = new DialogCloseListener();
        }

        private void Close(string result)
        {
            ButtonResult buttonResult = ButtonResult.None;
            if (result?.ToLower() == "true" || result?.ToLower() == "ok")
                buttonResult = ButtonResult.OK;
            else if (result?.ToLower() == "false" || result?.ToLower() == "cancel")
                buttonResult = ButtonResult.Cancel;

            // 使用 DialogCloseListener 来关闭对话框
            RequestClose.Invoke(new DialogResult(buttonResult));
        }

        

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
