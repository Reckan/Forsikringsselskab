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
    }
}