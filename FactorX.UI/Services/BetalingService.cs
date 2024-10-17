using System;
using FactorX.Core.Models;

namespace FactorX.UI.Services
{
    public class BetalingService
    {
        public void RegistreerBetaling(Factuur factuur, decimal bedrag, DateTime betaaldOp)
        {
            if (bedrag <= 0)
                throw new ArgumentException("Betaald bedrag moet groter zijn dan 0.");

            if (bedrag > factuur.OpenstaandBedrag)
                throw new ArgumentException("Betaald bedrag kan niet groter zijn dan het openstaande bedrag.");

            factuur.BetaaldBedrag += bedrag;
            factuur.BetaaldOp = betaaldOp;
        }
    }
}
