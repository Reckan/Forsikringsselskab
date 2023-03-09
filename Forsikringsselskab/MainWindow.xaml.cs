using DataClasses;
using FuncLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Forsikringsselskab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Func Func { get; set; } = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Func;
        }
        private void NyValgtKunde(Kunde kunde)
        {
            if (kunde != null)
            {
                Func.ValgtKunde = kunde;
            }
            else
            {
                Func.ValgtKunde = null;
            }
        }
        private void NyValgtBilmodel(Bilmodel bilmodel)
        {
            if (bilmodel != null)
            {
                Func.ValgtBilmodel = bilmodel;
            }
            else
            {
                Func.ValgtBilmodel = null;
                TbxPris.Text = "";
                TbxForsikringssumAuto.Text = "";
            }
        }

        private void BtnGem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Kunde kunde = GetKundeUiInfo();
                if (Func.ValgtKundeIRediger == null)
                {
                    OpretNyKunde(kunde);
                }
                else
                {
                    GemEksisterendeKunde(Func.ValgtKundeIRediger, kunde);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Fejl ved opret af kunde");
            }
        }

        private void GemEksisterendeKunde(Kunde kunde, Kunde kundeInfo)
        {
            Func.RedigerKunde(kunde, kundeInfo);
        }

        private void OpretNyKunde(Kunde kunde)
        {
            try
            {
                Func.NyKunde(kunde);
                TbxFornavn.Text = "";
                TbxEfternavn.Text = "";
                TbxAdresse.Text = "";
                TbxPostNummer.Text = "";
                TbxTelefon.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Kunde GetKundeUiInfo()
        {
            string fornavn;
            string efternavn;
            string adresse;
            int postnummer;
            int telefonNummer;
            try
            {
                if (TbxFornavn.Text == "")
                {
                    throw new ArgumentException(FornavnLabel.Content.ToString());
                }
                if (TbxEfternavn.Text == "")
                {
                    throw new ArgumentException(EfternavnLabel.Content.ToString());
                }
                if (TbxAdresse.Text == "")
                {
                    throw new ArgumentException(AdresseLabel.Content.ToString());
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Feltet \"{ex.ParamName}\" skal indholde tal");
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Feltet \"{ex.Message}\" skal udfyldes");
            }
            try
            {
                postnummer = int.Parse(TbxPostNummer.Text);
            }
            catch (FormatException)
            {
                throw new Exception($"Skriv tal if feltet \"{PostnummerLabel.Content.ToString()}\"");
            }
            try
            {
                telefonNummer = int.Parse(TbxTelefon.Text);
            }
            catch (FormatException)
            {
                throw new Exception($"Skriv tal if feltet \"{TelefonLabel.Content.ToString()}\"");
            }
            fornavn = TbxFornavn.Text;
            efternavn = TbxEfternavn.Text;
            adresse = TbxAdresse.Text;
            Kunde kunde = new(fornavn, efternavn, adresse, postnummer, telefonNummer);
            return kunde;
        }
        private Bilmodel GetBilUiIfo()
        {
            string mærke;
            string model;
            int startår;
            int slutår;
            int standartpris;
            int forsikringssum;
            try
            {
                if (TbxMærke.Text == "")
                {
                    throw new ArgumentException(MærkeLabel.Content.ToString());
                }
                if (TbxModel.Text == "")
                {
                    throw new ArgumentException(ModelLabel.Content.ToString());
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Feltet \"{ex.ParamName}\" skal indholde tal");
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Feltet \"{ex.Message}\" skal udfyldes");
            }


            try
            {
                startår = int.Parse(TbxStartår.Text);
            }
            catch (FormatException)
            {
                throw new Exception($"Skriv tal if feltet \"{StartårLabel.Content.ToString()}\"");
            }


            try
            {
                slutår = int.Parse(TbxSlutår.Text);
            }
            catch (FormatException)
            {
                throw new Exception($"Skriv tal if feltet \"{SlutårLabel.Content.ToString()}\"");
            }


            try
            {
                standartpris = int.Parse(TbxStandartpris.Text);
            }
            catch (FormatException)
            {
                throw new Exception($"Skriv tal if feltet \"{StandartprisLabel.Content.ToString()}\"");
            }


            try
            {
                forsikringssum = int.Parse(TbxForsikringssum.Text);
            }
            catch (FormatException)
            {
                throw new Exception($"Skriv tal if feltet \"{ForsikringssumLabel.Content.ToString()}\"");
            }

            mærke = TbxMærke.Text;
            model = TbxModel.Text;
            Bilmodel bilmodel = new(mærke, model, startår, slutår, standartpris, forsikringssum);
            return bilmodel;
        }
        private void OpretNyBilmodel(Bilmodel bilmodel)
        {
            try
            {
                Func.NyBilmodel(bilmodel);
                TbxModel.Text = "";
                TbxMærke.Text = "";
                TbxStartår.Text = "";
                TbxSlutår.Text = "";
                TbxStandartpris.Text = "";
                TbxForsikringssum.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GemEksisterendeBilmodel(Bilmodel bilmodel, Bilmodel bilmodelInfo)
        {
            Func.RedigerBilmodelInfo(bilmodel, bilmodelInfo);
        }

        private void BtnRediger_Click(object sender, RoutedEventArgs e)
        {
            Func.ValgtKundeIRediger = DgKundeList.SelectedItem as Kunde;

            TbxFornavn.Text = (Func.ValgtKundeIRediger != null) ? Func.ValgtKundeIRediger.Fornavn : "";
            TbxEfternavn.Text = Func.ValgtKundeIRediger?.Efternavn;
            TbxAdresse.Text = Func.ValgtKundeIRediger?.Adresse;
            TbxPostNummer.Text = Func.ValgtKundeIRediger?.Postnummer.ToString();
            TbxTelefon.Text = Func.ValgtKundeIRediger?.TelefonNummer.ToString();
        }

        private void BtnSlet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgKundeList.SelectedItem == null)
                {
                    throw (new ArgumentNullException("Kan ikke fjerne ikke valgt element"));
                }
                Func.SletKunde(DgKundeList.SelectedItem as Kunde);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.ParamName, "Fejl ved fjern");
            }
        }

        private void BtnGemBil_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bilmodel bilmodel = GetBilUiIfo();
                if (Func.ValgtBilmodelIRediger == null)
                {
                    OpretNyBilmodel(bilmodel);
                }
                else
                {
                    GemEksisterendeBilmodel(Func.ValgtBilmodelIRediger, bilmodel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Fejl ved opret af kunde");
            }
        }

        private void BtnRedigerBil_Click(object sender, RoutedEventArgs e)
        {

            Func.ValgtBilmodelIRediger = DgBilmodelList.SelectedItem as Bilmodel;

            TbxMærke.Text = (Func.ValgtBilmodelIRediger != null) ? Func.ValgtBilmodelIRediger.Mærke : "";
            TbxModel.Text = Func.ValgtBilmodelIRediger?.Model;
            TbxStartår.Text = Func.ValgtBilmodelIRediger?.Startår.ToString();
            TbxSlutår.Text = Func.ValgtBilmodelIRediger?.Slutår.ToString();
            TbxStandartpris.Text = Func.ValgtBilmodelIRediger?.Standardpris.ToString();
            TbxForsikringssum.Text = Func.ValgtBilmodelIRediger?.Forsikringssum.ToString();
        }

        private void BtnSletBil_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgBilmodelList.SelectedItem is null)
                {
                    throw (new ArgumentNullException("Kan ikke fjerne ikke valgt element"));
                }
                Func.SletBilmodel(DgBilmodelList.SelectedItem as Bilmodel);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.ParamName, "Fejl ved fjern");
            }
        }

        private void CbxKundeListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NyValgtKunde(CbxKundeListe.SelectedItem as Kunde);
        }

        private void CbxBilmodelList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NyValgtBilmodel(CbxBilmodelList.SelectedItem as Bilmodel);
        }

        private void BtnGemF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ForsikringAftaler forsikring = GetForsikringUiInfo();
                if (Func.ValgtForsikringAftaleIRediger is null)
                {
                    OpretNyForsikring(forsikring);
                }
                else
                {
                    GemEksisterendeForsikring(Func.ValgtForsikringAftaleIRediger, forsikring);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Fejl ved opret af forsikring");
            }
        }

        private void BtnRedigerF_Click(object sender, RoutedEventArgs e)
        {
            Func.ValgtForsikringAftaleIRediger = DgForsikringsList.SelectedItem as ForsikringAftaler;
            // Vælg Cbx stuff here

            TbxRegNr.Text = Func.ValgtForsikringAftaleIRediger?.Registreringsnummer;
            TbxPris.Text = Func.ValgtForsikringAftaleIRediger?.Pris.ToString();
            TbxForsikringssumAuto.Text = Func.ValgtForsikringAftaleIRediger?.Forsikringssum.ToString();
            TbxBetingelser.Text = Func.ValgtForsikringAftaleIRediger?.Betingelser;
        }

        private void BtnSletF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgForsikringsList.SelectedItem == null)
                {
                    throw (new ArgumentNullException("Kan ikke fjerne ikke valgt element"));
                }
                Func.SletForsikring(DgForsikringsList.SelectedItem as ForsikringAftaler);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.ParamName, "Fejl ved fjern");
            }
        }
        private void OpretNyForsikring(ForsikringAftaler forsikring)
        {
            try
            {
                Func.NyForsikring(forsikring);
                CbxKundeListe.SelectedIndex = -1;
                CbxBilmodelList.SelectedIndex = -1;
                TbxRegNr.Text = "";
                TbxPris.Text = "";
                TbxForsikringssumAuto.Text = "";
                TbxBetingelser.Text = "";
                DpDate.SelectedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GemEksisterendeForsikring(ForsikringAftaler forsikring, ForsikringAftaler forsikringInfo)
        {
            Func.RedigerForsikring(forsikring, forsikringInfo);
        }
        private ForsikringAftaler GetForsikringUiInfo()
        {
            Kunde kunde;
            Bilmodel bilmodel;
            string registreringsnummer;
            string betingelser;
            int pris;
            int forsikringssum;
            DateTime date;
            try
            {
                if (CbxKundeListe.SelectedItem is null)
                {
                    throw (new ArgumentNullException(nameof(CbxKundeListe)));
                }
                if (CbxBilmodelList.SelectedItem is null)
                {
                    throw (new ArgumentNullException(nameof(CbxBilmodelList)));
                }
                if (TbxRegNr.Text == "")
                {
                    throw (new ArgumentNullException(nameof(registreringsnummer)));
                }
                if (TbxBetingelser.Text == "")
                {
                    throw (new ArgumentNullException(nameof(betingelser)));
                }
                if (DpDate.SelectedDate == null)
                {
                    throw new ArgumentNullException(nameof(DpDate));
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                pris = int.Parse(TbxPris.Text);
            }
            catch (FormatException ex)
            {
                throw new Exception($"Skriv tal if feltet \"{PrisLabel.Content.ToString()}\"");
            }
            try
            {
                forsikringssum = int.Parse(TbxForsikringssumAuto.Text);
            }
            catch (FormatException)
            {
                throw new Exception($"Skriv tal if feltet \"{ForsikringssumLabelAuto.Content.ToString()}\"");
            }
            kunde = CbxKundeListe.SelectedItem as Kunde;
            bilmodel = CbxBilmodelList.SelectedItem as Bilmodel;
            registreringsnummer = TbxRegNr.Text;
            betingelser = TbxBetingelser.Text;
            date = (DateTime)DpDate.SelectedDate;
            ForsikringAftaler forsikring = new(kunde, bilmodel, registreringsnummer, betingelser, pris, forsikringssum, date);
            return forsikring;

        }
    }
}
