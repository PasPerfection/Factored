using System;
using System.Collections.Generic;
using System.Linq;
using FactorX.Core.Models;

namespace FactorX.UI.Services
{
    public class HerinneringService
    {
        private readonly EmailService _emailService;

        public HerinneringService(EmailService emailService)
        {
            _emailService = emailService;
        }

        public List<Factuur> GetVervallenFacturen(IEnumerable<Factuur> facturen)
        {
            return facturen.Where(f => f.IsVervallen).ToList();
        }

        public void VerstuurHerinnering(Factuur factuur)
        {
            if (factuur.IsVervallen)
            {
                factuur.AantalHerinneringen++;
                factuur.LaatsteHerinneringDatum = DateTime.Now;

                string onderwerp = $"Herinnering: Factuur {factuur.Nummer} is vervallen";
                string bericht = $"Geachte {factuur.Klant.Naam},\n\nDit is een herinnering dat factuur {factuur.Nummer} met een openstaand bedrag van {factuur.OpenstaandBedrag:C} is vervallen op {factuur.Vervaldatum:d}. Gelieve het openstaande bedrag zo spoedig mogelijk te voldoen.\n\nMet vriendelijke groet,\nUw bedrijf";

                _emailService.SendEmail(factuur.Klant.Email, onderwerp, bericht);
            }
        }
    }
}
