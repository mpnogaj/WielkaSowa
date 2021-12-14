using System.Collections.Generic;
using Avalonia.Controls;

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
