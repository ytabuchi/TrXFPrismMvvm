using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Dialogs
{
    class DemoDialogViewModel : BindableBase, IDialogAware
    {
        public event Action<IDialogParameters> RequestClose;

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public DelegateCommand CloseCommand { get; private set; }

        public DemoDialogViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.TryGetValue<string>("Title", out var title) ? title : "Title";
            Message = parameters.TryGetValue<string>("Message", out var message) ? message : "Message";
        }
    }
}
