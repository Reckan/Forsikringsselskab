using DataClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    internal class TableToObjectConverter
    {
        private SqlAccess sqlAccess;
        public TableToObjectConverter(SqlAccess sqlAccess)
        {
            this.sqlAccess = sqlAccess;
        }
        public ObservableCollection<Kunde> GetKundeList(DataTable table)
        {
            ObservableCollection<Kunde> list = new ObservableCollection<Kunde>();
            foreach (DataRow row in table.Rows)
            {
                Kunde kunde = GetKunde(row);
                list.Add(kunde);
            }
            return list;
        }
        private Kunde GetKunde(DataRow row)
        {
            Kunde kunde = new((string)row["Fornavn"], (string)row["Efternavn"], (string)row["Adresse"], (int)row["Postnummer"], (int)row["TelefonNummer"], (int)row["Id"]);
            return kunde;
        }
    }
}
