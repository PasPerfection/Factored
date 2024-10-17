using System;
using System.Collections.Generic;

namespace FactorX.Core.Models
{
    public class Offerte
    {
        public int Id { get; set; }
        public string Nummer { get; set; }
        public DateTime Datum { get; set; }
        public DateTime Geldigheid { get; set; }
        public Klant Klant { get; set; }
        public List<OfferteRegel> Regels { get; set; }
        public decimal Subtotaal => Regels?.Sum(r => r.Totaal) ?? 0;
        public decimal BTWPercentage { get; set; }
        public decimal BTWBedrag => Subtotaal * (BTWPercentage / 100);
        public decimal Totaal => Subtotaal + BTWBedrag;
        public string Status { get; set; }
    }

    public class OfferteRegel
    {
        public int Id { get; set; }
        public string Omschrijving { get; set; }
        public decimal Aantal { get; set; }
        public decimal Prijs { get; set; }
        public decimal Totaal => Aantal * Prijs;
    }
}
