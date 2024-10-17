using System;
using System.Collections.Generic;
using System.Linq;
using FactorX.Core.Models;

namespace FactorX.UI.Services
{
    public class RapportService
    {
        public List<RapportItem> GenereerOmzetRapport(IEnumerable<Factuur> facturen, DateTime startDatum, DateTime eindDatum)
        {
            return facturen
                .Where(f => f.Datum >= startDatum && f.Datum <= eindDatum)
                .GroupBy(f => new { Jaar = f.Datum.Year, Maand = f.Datum.Month })
                .Select(g => new RapportItem
                {
                    Periode = new DateTime(g.Key.Jaar, g.Key.Maand, 1),
                    Waarde = g.Sum(f => f.Totaal)
                })
                .OrderBy(r => r.Periode)
                .ToList();
        }

        public List<RapportItem> GenereerKlantenRapport(IEnumerable<Factuur> facturen, DateTime startDatum, DateTime eindDatum)
        {
            return facturen
                .Where(f => f.Datum >= startDatum && f.Datum <= eindDatum)
                .GroupBy(f => f.Klant.Naam)
                .Select(g => new RapportItem
                {
                    Label = g.Key,
                    Waarde = g.Sum(f => f.Totaal)
                })
                .OrderByDescending(r => r.Waarde)
                .Take(10)
                .ToList();
        }
    }

    public class RapportItem
    {
        public DateTime Periode { get; set; }
        public string Label { get; set; }
        public decimal Waarde { get; set; }
    }
}
