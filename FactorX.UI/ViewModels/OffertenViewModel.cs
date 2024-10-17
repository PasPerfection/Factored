using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using FactorX.Core.Models;
using System.Linq;
using FactorX.UI.Views;
using FactorX.UI.Services;
using Microsoft.Win32;

namespace FactorX.UI.ViewModels
{
    public class OffertenViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Offerte> _offertes;
        private Offerte _geselecteerdeOfferte;
        private Offerte _huidigeOfferte;

        public ObservableCollection<Offerte> Offertes
        {
            get => _offertes;
            set
            {
                _offertes = value;
                OnPropertyChanged(nameof(Offertes));
            }
        }

        public Offerte GeselecteerdeOfferte
        {
            get => _geselecteerdeOfferte;
            set
            {
                _geselecteerdeOfferte = value;
                OnPropertyChanged(nameof(GeselecteerdeOfferte));
            }
        }

        public Offerte HuidigeOfferte
        {
            get => _huidigeOfferte;
            set
            {
                _huidigeOfferte = value;
                OnPropertyChanged(nameof(HuidigeOfferte));
            }
        }

        public ICommand NieuweOfferteCommand { get; private set; }
        public ICommand BewerkenCommand { get; private set; }
        public ICommand VerwijderenCommand { get; private set; }
        public ICommand OpslaanCommand { get; private set; }
        public ICommand AnnulerenCommand { get; private set; }
        public ICommand ExporteerNaarPdfCommand { get; private set; }

        public OffertenViewModel()
        {
            Offertes = new ObservableCollection<Offerte>();
            NieuweOfferteCommand = new RelayCommand(MaakNieuweOfferte);
            BewerkenCommand = new RelayCommand(BewerkOfferte, CanBewerkOfferte);
            VerwijderenCommand = new RelayCommand(VerwijderOfferte, CanVerwijderOfferte);
            OpslaanCommand = new RelayCommand(SlaOfferteOp, CanSlaOfferteOp);
            AnnulerenCommand = new RelayCommand(AnnuleerBewerking, CanAnnuleerBewerking);
            ExporteerNaarPdfCommand = new RelayCommand(ExporteerNaarPdf, CanExporteerNaarPdf);
        }

        private void MaakNieuweOfferte()
        {
            HuidigeOfferte = new Offerte
            {
                Id = Offertes.Count + 1,
                Datum = DateTime.Now,
                Geldigheid = DateTime.Now.AddDays(30),
                BTWPercentage = 21,
                Status = "Concept",
                Regels = new List<OfferteRegel>()
            };
            OpenOfferteDetailsView();
        }

        private void BewerkOfferte()
        {
            if (GeselecteerdeOfferte != null)
            {
                HuidigeOfferte = new Offerte
                {
                    Id = GeselecteerdeOfferte.Id,
                    Nummer = GeselecteerdeOfferte.Nummer,
                    Datum = GeselecteerdeOfferte.Datum,
                    Geldigheid = GeselecteerdeOfferte.Geldigheid,
                    Klant = GeselecteerdeOfferte.Klant,
                    Regels = new List<OfferteRegel>(GeselecteerdeOfferte.Regels),
                    BTWPercentage = GeselecteerdeOfferte.BTWPercentage,
                    Status = GeselecteerdeOfferte.Status
                };
                OpenOfferteDetailsView();
            }
        }

        private bool CanBewerkOfferte() => GeselecteerdeOfferte != null;

        private void VerwijderOfferte()
        {
            if (GeselecteerdeOfferte != null)
            {
                Offertes.Remove(GeselecteerdeOfferte);
            }
        }

        private bool CanVerwijderOfferte() => GeselecteerdeOfferte != null;

        private void SlaOfferteOp()
        {
            if (HuidigeOfferte.Id == 0)
            {
                HuidigeOfferte.Id = Offertes.Count + 1;
                Offertes.Add(HuidigeOfferte);
            }
            else
            {
                var bestaandeOfferte = Offertes.FirstOrDefault(o => o.Id == HuidigeOfferte.Id);
                if (bestaandeOfferte != null)
                {
                    int index = Offertes.IndexOf(bestaandeOfferte);
                    Offertes[index] = HuidigeOfferte;
                }
            }
            HuidigeOfferte = null;
            CloseOfferteDetailsView();
        }

        private bool CanSlaOfferteOp() => HuidigeOfferte != null;

        private void AnnuleerBewerking()
        {
            HuidigeOfferte = null;
            CloseOfferteDetailsView();
        }

        private bool CanAnnuleerBewerking() => HuidigeOfferte != null;

        private void ExporteerNaarPdf()
        {
            if (GeselecteerdeOfferte != null)
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    DefaultExt = "pdf",
                    FileName = $"Offerte_{GeselecteerdeOfferte.Nummer}.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    var pdfExporter = new PdfExporter();
                    pdfExporter.ExportOfferteToPdf(GeselecteerdeOfferte, saveFileDialog.FileName);
                    // Toon een bevestigingsbericht aan de gebruiker
                }
            }
        }

        private bool CanExporteerNaarPdf() => GeselecteerdeOfferte != null;

        private void OpenOfferteDetailsView()
        {
            var offerteDetailsView = new OfferteDetailsView(this);
            offerteDetailsView.ShowDialog();
        }

        private void CloseOfferteDetailsView()
        {
            // Implementeer deze methode om het OfferteDetailsView-venster te sluiten
            // Dit kan worden gedaan door een event te triggeren dat door de view wordt afgehandeld
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
