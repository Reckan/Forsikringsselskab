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
        private Kunde _ValgtKunde;
        public Kunde ValgtKunde
        {
            get
            {
                return _ValgtKunde;
            }
            set
            {
                _ValgtKunde = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(ValgtKunde)));
                }
            }
        }
        private Kunde _ValgtKundeIRediger;
        public Kunde ValgtKundeIRediger
        {
            get
            {
                return _ValgtKundeIRediger;
            }
            set
            {
                _ValgtKundeIRediger = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(ValgtKundeIRediger)));
                }
            }
        }
        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        private void ValidateInfo(Kunde kunde)
        {
            if (kunde.Fornavn == "")
            {
                throw new ArgumentException("Fornavn skal udfyldes");
            }
            if (kunde.Efternavn == "")
            {
                throw new ArgumentException("Efternavn skal udfyldes");
            }
            if (kunde.Adresse == "")
            {
                throw new ArgumentException("Adresse skal udfyldes");
            }
            if (kunde.Postnummer.ToString().Length < 4 || kunde.Postnummer.ToString().Length > 4)
            {
                throw new ArgumentException("Postnummer kan have kun have 4 cifre");
            }
            foreach (Kunde kunde1 in KundeList)
            {
                if (kunde.TelefonNummer == kunde1.TelefonNummer)
                {
                    throw new ArgumentException("Telefon Nummeret er allerede i brug");
                }
            }

        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NyKunde(Kunde kundeInfo)
        {
            ValidateInfo(kundeInfo);
            Data.NyKunde(kundeInfo);
            RaisePropertyChanged(nameof(KundeList));
        }
        public void RedigerKunde(Kunde kunde, Kunde kundeInfo)
        {
            Data.UpdateKundeInfo(kunde, kundeInfo);
            RaisePropertyChanged(nameof(KundeList));
        }
        public void SletKunde(Kunde kunde)
        {
            Data.DeleteKunde(kunde);
            RaisePropertyChanged(nameof(KundeList));
        }
    }
}