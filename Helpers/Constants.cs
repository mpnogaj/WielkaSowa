using Avalonia.Controls;
using System.Collections.Generic;

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
