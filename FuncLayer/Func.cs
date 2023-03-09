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
        public ObservableCollection<Bilmodel> BilmodelList
        {
            get
            {
                return Data.BilmodelList;
            }
        }
        public ObservableCollection<ForsikringAftaler> ForsikringList
        {
            get
            {
                return Data.ForsikringList;
            }
        }
        private ForsikringAftaler? _ValgtForsikringAftale;
        public ForsikringAftaler ValgtForsikringAftale
        {
            get
            {
                return _ValgtForsikringAftale;
            }
            set
            {
                _ValgtForsikringAftale = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(ForsikringList)));
                }
            }
        }
        private ForsikringAftaler? _ValgtForsikringAftaleIRediger;
        public ForsikringAftaler ValgtForsikringAftaleIRediger
        {
            get
            {
                return _ValgtForsikringAftaleIRediger;
            }
            set
            {
                _ValgtForsikringAftaleIRediger = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(ValgtForsikringAftaleIRediger)));
                }
            }
        }
        private Kunde? _ValgtKunde;
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
        private Kunde? _ValgtKundeIRediger;
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
        private Bilmodel? _ValgtBilmodel;
        public Bilmodel ValgtBilmodel
        {
            get
            {
                return _ValgtBilmodel;
            }
            set
            {
                _ValgtBilmodel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(ValgtBilmodel)));
                }
            }
        }
        private Bilmodel? _ValgtBilmodelIRediger;
        public Bilmodel ValgtBilmodelIRediger
        {
            get
            {
                return _ValgtBilmodelIRediger;
            }
            set
            {
                _ValgtBilmodelIRediger = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(ValgtBilmodelIRediger)));
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
            if (kunde.Postnummer.ToString().Length != 4)
            {
                throw new ArgumentException("Postnummer kan have kun have 4 cifre");
            }
            if (kunde.TelefonNummer.ToString().Length != 8)
            {
                throw new ArgumentException("Telefon Nummer skal være gyldigt");
            }
            foreach (Kunde kunde1 in KundeList)
            {
                if (kunde.TelefonNummer == kunde1.TelefonNummer)
                {
                    throw new ArgumentException("Telefon Nummeret er allerede i brug");
                }
            }
        }
        private void ValidateBilInfo(Bilmodel bilmodel)
        {
            if (bilmodel.Model == "")
            {
                throw new ArgumentException("Model skal udfyldes");
            }
            if (bilmodel.Mærke == "")
            {
                throw new ArgumentException("Mærke skal udfuldes");
            }
            if (bilmodel.Slutår < bilmodel.Startår)
            {
                throw new ArgumentException("Startår kan ikke være støre end Slutår");
            }
            foreach (Bilmodel bilmodel1 in BilmodelList)
            {
                if (bilmodel.Mærke == bilmodel1.Mærke && bilmodel.Model == bilmodel1.Model && bilmodel.Startår >= bilmodel1.Startår && bilmodel.Slutår <= bilmodel1.Slutår && bilmodel.Startår <= bilmodel1.Slutår)
                {
                    throw new ArgumentException("Der en bil af samme Mærke, Model og Årstal findes allerede");
                }
            }
        }
        private void ValidateForsikringInfo(ForsikringAftaler forsikring)
        {
            if (forsikring.Kunde == null)
            {
                throw new ArgumentException("Skal Vælge en Kunde");
            }
            if (forsikring.Bilmodel == null)
            {
                throw new ArgumentException("Skal Vælge En Bilmodel");
            }
            if (forsikring.Registreringsnummer == null)
            {
                throw new ArgumentException("Skale udfylde Registreringsnummet");
            }
            if (forsikring.Pris > forsikring.Forsikringssum)
            {
                throw new ArgumentException("Pris kan ikke være støre end Forsikringssum");
            }
            if (forsikring.Betingelser == null)
            {
                throw new ArgumentException("Betingelser skal udfyldes");
            }
            //if (forsikring.ForsikringPeriode == null)
            //{
            //    throw new ArgumentNullException("Skal vælge en star dato");
            //}
            foreach (ForsikringAftaler forsikring1 in ForsikringList)
            {
                if (forsikring.Registreringsnummer == forsikring1.Registreringsnummer)
                {
                    throw new ArgumentException("Dette Registreringsnummer er allerede i brug");
                }
            }
        }
        private void CheckIfDeletable(Kunde kunde)
        {
            foreach (ForsikringAftaler forsikring in ForsikringList)
            {
                if (forsikring.Kunde.Id == kunde.Id)
                {
                    throw new Exception("Kan ikke slette en kune men en aktiv forsikring");
                }
            }
        }
        private void CheckIfDeletableBil(Bilmodel bilmodel)
        {
            foreach(ForsikringAftaler forsikring in ForsikringList)
            {
                if(forsikring.Bilmodel.Id == bilmodel.Id)
                {
                    throw new Exception("Kan ikke slette en bilmodle der er aktiv i brug af kunder");
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
            CheckIfDeletable(kunde);
            Data.DeleteKunde(kunde);
            RaisePropertyChanged(nameof(KundeList));
        }
        public void NyBilmodel(Bilmodel bilmodelInfo)
        {
            ValidateBilInfo(bilmodelInfo);
            Data.NyBilmodel(bilmodelInfo);
            RaisePropertyChanged(nameof(BilmodelList));
        }
        public void RedigerBilmodelInfo(Bilmodel bilmodel, Bilmodel bilmodelInfo)
        {
            Data.UpdateBilmodel(bilmodel, bilmodelInfo);
            RaisePropertyChanged(nameof(BilmodelList));
        }
        public void SletBilmodel(Bilmodel bilmodel)
        {
            CheckIfDeletableBil(bilmodel);
            Data.DeleteBilmodel(bilmodel);
            RaisePropertyChanged(nameof(BilmodelList));
        }
        public void NyForsikring(ForsikringAftaler forsikring)
        {
            ValidateForsikringInfo(forsikring);
            if (forsikring.ForsikringPeriode < DateTime.Today)
            {
                throw new Exception("Can't Time Travel yet");
            }
            else
            {
                Data.NyForsikring(forsikring);
            }
            RaisePropertyChanged(nameof(ForsikringList));
        }
        public void RedigerForsikring(ForsikringAftaler forsikring, ForsikringAftaler forsikringInfo)
        {
            Data.UpdateForsikring(forsikring, forsikringInfo);
            RaisePropertyChanged(nameof(ForsikringList));
        }
        public void SletForsikring(ForsikringAftaler forsikring)
        {
            Data.DeleteForsikring(forsikring);
            RaisePropertyChanged(nameof(ForsikringList));
        }
    }
}