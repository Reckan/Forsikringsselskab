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
        public ObservableCollection<Bilmodel> GetBilmodelList(DataTable table)
        {
            ObservableCollection<Bilmodel> list = new ObservableCollection<Bilmodel>();
            foreach (DataRow row in table.Rows)
            {
                Bilmodel bilmodel = GetBilmodel(row);
                list.Add(bilmodel);
            }
            return list;
        }
        public ObservableCollection<ForsikringAftaler> GetForsikringList(DataTable table)
        {
            ObservableCollection<ForsikringAftaler> list = new ObservableCollection<ForsikringAftaler>();
            foreach (DataRow row in table.Rows)
            {
                ForsikringAftaler aftaler = GetAftaler(row);
                list.Add(aftaler);
            }
            return list;
        }
        private Kunde GetKunde(DataRow row)
        {
            Kunde kunde = new((string)row["Fornavn"], (string)row["Efternavn"], (string)row["Adresse"], (int)row["Postnummer"], (int)row["TelefonNummer"], (int)row["Id"]);
            return kunde;
        }
        private Bilmodel GetBilmodel(DataRow row)
        {
            Bilmodel bilmodel = new((string)row["Mærke"], (string)row["Model"], (int)row["Startår"], (int)row["Slutår"], (int)row["Standardpris"], (int)row["Forsikringssum"], (int)row["Id"]);
            return bilmodel;
        }
        private ForsikringAftaler GetAftaler(DataRow row)
        {
            ForsikringAftaler aftaler = new();
            return aftaler;
        }
    }
}
