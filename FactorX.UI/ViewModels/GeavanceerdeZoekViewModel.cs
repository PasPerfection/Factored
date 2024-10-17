using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using FactorX.Core.Models;
using FactorX.UI.Services;

namespace FactorX.UI.ViewModels
{
    public class GeavanceerdeZoekViewModel : INotifyPropertyChanged
    {
        private readonly ZoekService _zoekService;
        private readonly KlantenViewModel _klantenViewModel;
        private readonly FacturenViewModel _facturenViewModel;
        private readonly OffertenViewModel _offertenViewModel;

        private string _zoekTerm;
        private DateTime? _startDatum;
        private DateTime? _eindDatum;
        private decimal? _minimumBedrag;
        private decimal? _maximumBedrag;
        private string _status;

        public string ZoekTerm
        {
            get => _zoekTerm;
            set
            {
                _zoekTerm = value;
                OnPropertyChanged(nameof(ZoekTerm));
            }
        }

        public DateTime? StartDatum
        {
            get => _startDatum;
            set
            {
                _startDatum = value;
                OnPropertyChanged(nameof(StartDatum));
            }
        }

        public DateTime? EindDatum
        {
            get => _eindDatum;
            set
            {
                _eindDatum = value;
                OnPropertyChanged(nameof(EindDatum));
            }
        }

        public decimal? MinimumBedrag
        {
            get => _minimumBedrag;
            set
            {
                _minimumBedrag = value;
                OnPropertyChanged(nameof(MinimumBedrag));
            }
        }

        public decimal? MaximumBedrag
        {
            get => _maximumBedrag;
            set
            {
                _maximumBedrag = value;
                OnPropertyChanged(nameof(MaximumBedrag));
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public ObservableCollection<object> ZoekResultaten { get; set; }

        public ICommand ZoekCommand { get; private set; }

        public GeavanceerdeZoekViewModel(ZoekService zoekService, KlantenViewModel klantenViewModel, FacturenViewModel facturenViewModel, OffertenViewModel offertenViewModel)
        {
            _zoekService = zoekService;
            _klantenViewModel = klantenViewModel;
            _facturenViewModel = facturenViewModel;
            _offertenViewModel = offertenViewModel;

            ZoekResultaten = new ObservableCollection<object>();
            ZoekCommand = new RelayCommand(VoerZoekdrachtUit);
        }

        private void VoerZoekdrachtUit()
        {
            ZoekResultaten.Clear();

            var klantenResultaten = _zoekService.ZoekKlanten(_klantenViewModel.Klanten, ZoekTerm);
            var facturenResultaten = _zoekService.ZoekFacturen(_facturenViewModel.Facturen, ZoekTerm, StartDatum, EindDatum, MinimumBedrag, MaximumBedrag, Status);
            var offertenResultaten = _zoekService.ZoekOffertes(_offertenViewModel.Offertes, ZoekTerm, StartDatum, EindDatum, MinimumBedrag, MaximumBedrag, Status);

            foreach (var klant in klantenResultaten)
            {
                ZoekResultaten.Add(klant);
            }

            foreach (var factuur in facturenResultaten)
            {
                ZoekResultaten.Add(factuur);
            }

            foreach (var offerte in offertenResultaten)
            {
                ZoekResultaten.Add(offerte);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
