using DataClasses;
using System.Collections.ObjectModel;
using System.Text;

namespace DataLayer
{
    public class Data
    {
        SqlAccess sqlAccess;
        TableToObjectConverter converter;
        public ObservableCollection<Kunde> KundeList
        {
            get
            {
                return converter.GetKundeList(sqlAccess.ExecuteSql("select * from Kunder"));
            }
        }

        public ObservableCollection<Bilmodel> BilmodelList
        {
            get
            {
                return converter.GetBilmodelList(sqlAccess.ExecuteSql("select * from Bilmodel"));
            }
        }
        public ObservableCollection<ForsikringAftaler> ForsikringList
        {
            get
            {
                return converter.GetForsikringList(sqlAccess.ExecuteSql(""));
            }
        }
        public Data()
        {
            sqlAccess = new SqlAccess();
            converter = new TableToObjectConverter(sqlAccess);
        }
        public void NyKunde(Kunde kundeInfo)
        {
            sqlAccess.ExecuteSql($"insert into [dbo].[Kunder] ([Fornavn], [Efternavn], [Adresse], [Postnummer], [TelefonNummer]) values('{kundeInfo.Fornavn}', '{kundeInfo.Efternavn}', '{kundeInfo.Adresse}', {kundeInfo.Postnummer.ToString()}, {kundeInfo.TelefonNummer.ToString()})");
        }
        public void UpdateKundeInfo(Kunde kunde, Kunde kundeInfo)
        {
            sqlAccess.ExecuteSql($"update [dbo].[Kunder] set Fornavn = '{kundeInfo.Fornavn}', Efternavn = '{kundeInfo.Efternavn}', Adresse = '{kundeInfo.Adresse}', Postnummer = {kundeInfo.Postnummer.ToString()}, TelefonNummer = {kundeInfo.TelefonNummer.ToString()} where Id = {kunde.Id}");
        }
        public void DeleteKunde( Kunde kunde)
        {
            sqlAccess.ExecuteSql($"delete from [dbo].[Kunder] where Id = {kunde.Id}");
        }
        public void NyBilmodel(Bilmodel bilmodelInfo)
        {
            sqlAccess.ExecuteSql($"insert into [dbo].[Bilmodel] ([Mærke], [Model], [Startår], [Slutår], [Standardpris], [Forsikringssum]) values('{bilmodelInfo.Mærke}', '{bilmodelInfo.Model}', {bilmodelInfo.Startår.ToString()}, {bilmodelInfo.Slutår.ToString()}, {bilmodelInfo.Standardpris.ToString()}, {bilmodelInfo.Forsikringssum.ToString()})");
        }
        public void UpdateBilmodel(Bilmodel bilmodel, Bilmodel bilmodelInfo)
        {
            sqlAccess.ExecuteSql($"update [dbo].[Bilmodel] set Mærke = '{bilmodelInfo.Mærke}', Model = '{bilmodelInfo.Model}', Startår = {bilmodelInfo.Startår.ToString()}, Slutår = {bilmodelInfo.Slutår.ToString()}, Standardpris = {bilmodelInfo.Standardpris.ToString()}, Forsikringssum = {bilmodelInfo.Forsikringssum.ToString()} where Id = {bilmodel.Id}");
        }
        public void DeleteBilmodel(Bilmodel bilmodel)
        {
            sqlAccess.ExecuteSql($"delete from [dbo].[Bilmodel] where Id = {bilmodel.Id}");
        }
    }
}