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
            Bilmodel bilmodel = new(mærke, model, startår, slutår,standartpris, forsikringssum);
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
                if(DgBilmodelList.SelectedItem == null)
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

    }
}
