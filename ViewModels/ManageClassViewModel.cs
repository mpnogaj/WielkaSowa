using System;
using WielkaSowa.Helpers;
using WielkaSowa.Models;
using WielkaSowa.ViewModels.Commands;

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

        #region Commands
        public RelayCommand OkCommand { get; }
        public RelayCommand CancelCommand { get; }
        #endregion

        public ManageClassViewModel(Action<Class> onOk, Action? onCancel, Class? c = null)
        {
            _class = c ?? new Class();
            Storage.UpdateCalcs();
            OkCommand = new RelayCommand(() =>
            {
                Storage.UpdateCalcs();
                onOk(Class);
                Essentials.CloseTopWindow();
            });
            CancelCommand = new RelayCommand(() =>
            {
                Storage.Instance!.Classes.Remove(_class);
                Storage.UpdateCalcs();
                onCancel?.Invoke();
                Essentials.CloseTopWindow();
            });
        }
    }
}