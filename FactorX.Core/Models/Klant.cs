using System;

namespace FactorX.Core.Models
{
    public class Klant
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Bedrijfsnaam { get; set; }
        public string Email { get; set; }
        public string Telefoonnummer { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Plaats { get; set; }
        public string BTWNummer { get; set; }
        public DateTime AanmaakDatum { get; set; }
    }
}
