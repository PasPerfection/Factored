using System;

namespace FactorX.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public decimal Prijs { get; set; }
        public string Eenheid { get; set; }
        public string BTWPercentage { get; set; }
    }
}
