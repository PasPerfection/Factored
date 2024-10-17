public class Factuur
{
    // ... bestaande eigenschappen ...

    public decimal BetaaldBedrag { get; set; }
    public DateTime? BetaaldOp { get; set; }
    public string BetalingsStatus => BetaaldBedrag >= Totaal ? "Volledig betaald" : (BetaaldBedrag > 0 ? "Gedeeltelijk betaald" : "Niet betaald");
    public decimal OpenstaandBedrag => Totaal - BetaaldBedrag;
}
