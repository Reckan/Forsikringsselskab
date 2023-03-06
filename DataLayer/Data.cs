using DataClasses;
using System.Collections.ObjectModel;

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
    }
}