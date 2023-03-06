using DataClasses;
using DataLayer;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FuncLayer
{
    public class Func : INotifyPropertyChanged
    {
        private Data Data { get; } = new();
        public ObservableCollection<Kunde> KundeList
        {
            get
            {
                return Data.KundeList;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}