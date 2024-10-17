using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using FactorX.Core.Models;
using FactorX.UI.Services;
using System.Linq;

namespace FactorX.UI.ViewModels
{
    public class RapportViewModel : INotifyPropertyChanged
    {
        private readonly RapportService _rapportService;
        private readonly FacturenViewModel _facturenViewModel;

        private DateTime _startDatum;
        private DateTime _eindDatum;
        private List<RapportItem> _rapportItems;

        public DateTime StartDatum
        {
            get => _startDatum;
            set
            {
                _startDatum = value;
                OnPropertyChanged(nameof(StartDatum));
            }
        }

        public DateTime EindDatum
        {
            get => _eindDatum;
            set
            {
                _eindDatum = value;
                OnPropertyChanged(nameof(EindDatum));
            }
        }

        public List<RapportItem> RapportItems
        {
            get => _rapportItems;
            set
            {
                _rapportItems = value;
                OnPropertyChanged(nameof(RapportItems));
            }
        }

        public ICommand GenereerOmzetRapportCommand { get; private set; }
        public ICommand GenereerKlantenRapportCommand { get; private set; }

        public RapportViewModel(RapportService rapportService, FacturenViewModel facturenViewModel)
        {
            _rapportService = rapportService;
            _facturenViewModel = facturenViewModel;

            StartDatum = DateTime.Now.AddMonths(-12);
            EindDatum = DateTime.Now;

            GenereerOmzetRapportCommand = new RelayCommand(GenereerOmzetRapport);
            GenereerKlantenRapportCommand = new RelayCommand(GenereerKlantenRapport);
        }

        private void GenereerOmzetRapport()
        {
            RapportItems = _rapportService.GenereerOmzetRapport(_facturenViewModel.Facturen, StartDatum, EindDatum);
            // Hier zou je aanvullende logica kunnen toevoegen voor het voorbereiden van grafiekdata
        }

        private void GenereerKlantenRapport()
        {
            RapportItems = _rapportService.GenereerKlantenRapport(_facturenViewModel.Facturen, StartDatum, EindDatum);
            // Hier zou je aanvullende logica kunnen toevoegen voor het voorbereiden van grafiekdata
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
