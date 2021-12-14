using WielkaSowa.Helpers;
using WielkaSowa.ViewModels.Commands;

namespace WielkaSowa.ViewModels
{
    public class ContactViewModel : ViewModelBase
    {
        public RelayCommand CloseWindowCommand { get; }

        public ContactViewModel()
        {
            CloseWindowCommand = new RelayCommand(Essentials.CloseTopWindow);
        }
    }
}
