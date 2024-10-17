using System;
using System.Collections.Generic;
using System.Linq;
using FactorX.Core.Models;

namespace FactorX.UI.Services
{
    public class ZoekService
    {
        public IEnumerable<Klant> ZoekKlanten(IEnumerable<Klant> klanten, string zoekterm)
        {
            return klanten.Where(k =>
                k.Naam.Contains(zoekterm, StringComparison.OrdinalIgnoreCase) ||
                k.Bedrijfsnaam.Contains(zoekterm, StringComparison.OrdinalIgnoreCase) ||
                k.Email.Contains(zoekterm, StringComparison.OrdinalIgnoreCase)
            );
        }

        public IEnumerable<Factuur> ZoekFacturen(IEnumerable<Factuur> facturen, string zoekterm)
        {
            return facturen.Where(f =>
                f.Nummer.Contains(zoekterm, StringComparison.OrdinalIgnoreCase) ||
                f.Klant.Naam.Contains(zoekterm, StringComparison.OrdinalIgnoreCase) ||
                f.Status.Contains(zoekterm, StringComparison.OrdinalIgnoreCase)
            );
        }

        public IEnumerable<Offerte> ZoekOffertes(IEnumerable<Offerte> offertes, string zoekterm)
        {
            return offertes.Where(o =>
                o.Nummer.Contains(zoekterm, StringComparison.OrdinalIgnoreCase) ||
                o.Klant.Naam.Contains(zoekterm, StringComparison.OrdinalIgnoreCase) ||
                o.Status.Contains(zoekterm, StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}
