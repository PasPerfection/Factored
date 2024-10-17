using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using FactorX.Core.Models;

namespace FactorX.UI.Services
{
    public class PdfExporter
    {
        public void ExportFactuurToPdf(Factuur factuur, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                var document = new Document(PageSize.A4, 25, 25, 30, 30);
                var writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                // Voeg bedrijfslogo toe (indien beschikbaar)
                // Image logo = Image.GetInstance("pad/naar/logo.png");
                // document.Add(logo);

                // Voeg factuurgegevens toe
                var font = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

                document.Add(new Paragraph($"Factuurnummer: {factuur.Nummer}", boldFont));
                document.Add(new Paragraph($"Datum: {factuur.Datum:d}", font));
                document.Add(new Paragraph($"Vervaldatum: {factuur.Vervaldatum:d}", font));
                document.Add(new Paragraph("\n"));

                // Voeg klantgegevens toe
                document.Add(new Paragraph("Klantgegevens:", boldFont));
                document.Add(new Paragraph($"{factuur.Klant.Naam}", font));
                document.Add(new Paragraph($"{factuur.Klant.Adres}", font));
                document.Add(new Paragraph($"{factuur.Klant.Postcode} {factuur.Klant.Plaats}", font));
                document.Add(new Paragraph("\n"));

                // Voeg factuurregels toe
                var table = new PdfPTable(4);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 4f, 1f, 2f, 2f });

                table.AddCell(new PdfPCell(new Phrase("Omschrijving", boldFont)));
                table.AddCell(new PdfPCell(new Phrase("Aantal", boldFont)));
                table.AddCell(new PdfPCell(new Phrase("Prijs", boldFont)));
                table.AddCell(new PdfPCell(new Phrase("Totaal", boldFont)));

                foreach (var regel in factuur.Regels)
                {
                    table.AddCell(new PdfPCell(new Phrase(regel.Omschrijving, font)));
                    table.AddCell(new PdfPCell(new Phrase(regel.Aantal.ToString(), font)));
                    table.AddCell(new PdfPCell(new Phrase(regel.Prijs.ToString("C"), font)));
                    table.AddCell(new PdfPCell(new Phrase(regel.Totaal.ToString("C"), font)));
                }

                document.Add(table);

                // Voeg totalen toe
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph($"Subtotaal: {factuur.Subtotaal:C}", font));
                document.Add(new Paragraph($"BTW ({factuur.BTWPercentage}%): {factuur.BTWBedrag:C}", font));
                document.Add(new Paragraph($"Totaal: {factuur.Totaal:C}", boldFont));

                document.Close();
            }
        }

        public void ExportOfferteToPdf(Offerte offerte, string filePath)
        {
            // Implementeer vergelijkbare logica als ExportFactuurToPdf, maar pas aan voor offertes
        }
    }
}
