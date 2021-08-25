using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;

namespace WielkaSowa.Helpers
{
    public static class Constants
    {
        public static readonly List<FileDialogFilter> DataFileFilters = new()
        {
            new FileDialogFilter()
            {
                Extensions = new(){ "json" },
                Name = "Plik danych"
            }
        };
    }
}
