using SampleEventAggregator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModuleSubA2.ViewModels
{
    public class ModuleSubA2ViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;

        private string _message = string.Empty;

        public string Message
        {
            get { return _message; }
            set { _message = value; RaisePropertyChanged(nameof(Message)); }
        }

        public ModuleSubA2ViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        }

        private void MessageReceived(string msg)
        {
            Message = msg;
        }
    }
}
