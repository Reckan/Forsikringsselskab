using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses
{
    public class ForsikringAftaler
    {
        public ForsikringAftaler(Kunde kunde, Bilmodel bilmodel, string registreringsnummer, string betingelser, int pris, int forsikringssum, DateTime forsikringPeriode, int id = -1)
        {
            Kunde = kunde;
            Bilmodel = bilmodel;
            Registreringsnummer = registreringsnummer;
            Betingelser = betingelser;
            Pris = pris;
            Forsikringssum = forsikringssum;
            ForsikringPeriode = forsikringPeriode;
            Id = id;
        }
        public Kunde Kunde { get; }
        public Bilmodel Bilmodel { get; }
        public string Registreringsnummer { get; set; }
        public int Pris { get; set; }
        public int Forsikringssum { get; set; }
        public string Betingelser { get; set; }
        public DateTime ForsikringPeriode { get; private set; }
        public int Id { get; }
    }
}
