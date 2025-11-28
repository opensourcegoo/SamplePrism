
using SampleEventAggregator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleSubA1.ViewModels
{
    internal class ModuleSubA1ViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;
        public DelegateCommand SendMessageCommand { get; set; }

        private string _message = "Test Message";

        public string Message
        {
            get { return _message; }
            set { _message = value;RaisePropertyChanged(nameof(Message)); }
        }

        public ModuleSubA1ViewModel( IEventAggregator eventAggregator)
        {
            SendMessageCommand = new DelegateCommand(SendMessage);
            _eventAggregator = eventAggregator;
        }
         
        private void SendMessage()
        {
            _eventAggregator.GetEvent<MessageSentEvent>().Publish(Message);
        }
    }
}
