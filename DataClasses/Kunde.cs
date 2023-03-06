﻿namespace DataClasses
{
    public class Kunde
    {
        public Kunde(string fornavn, string efternavn, string adresse, int postnummer, int telefonNummer, int id)
        {
            Fornavn = fornavn;
            Efternavn = efternavn;
            Adresse = adresse;
            Postnummer = postnummer;
            TelefonNummer = telefonNummer;
            Id = id;
        }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public string Adresse { get; set; }
        public int Postnummer { get; set; }
        public int TelefonNummer { get; set; }
        public int Id { get; }
    }
}