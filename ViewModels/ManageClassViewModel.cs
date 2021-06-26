using System;
using System.Collections.Generic;
using WielkaSowa.Models;

namespace WielkaSowa.ViewModels
{
    public class ManageClassViewModel : ViewModelBase
    {
        private Class _class;
        public Class Class
        {
            get => _class;
            set => SetProperty(ref _class, value);
        }
        
        public RelayCommand OkCommand { get; }

        public RelayCommand CancelCommand { get; }

        public ManageClassViewModel(Action<Class> onOk, Action? onCancel, Class? c = null)
        {
            _class = c ?? new Class();
            OkCommand = new RelayCommand(() =>
            {
                onOk(Class);
                Essentials.CloseTopWindow();
            });
            CancelCommand = new RelayCommand(() =>
            {
                if (onCancel != null) onCancel();
                Essentials.CloseTopWindow();
            });
        }
    }
}