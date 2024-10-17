using System;
using System.ComponentModel;
using System.Linq;
using FactorX.Core.Models;

namespace FactorX.UI.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly KlantenViewModel _klantenViewModel;
        private readonly FacturenViewModel _facturenViewModel;
        private readonly OffertenViewModel _offertenViewModel;

        public int TotaalAantalKlanten => _klantenViewModel.Klanten.Count;
        public int TotaalAantalFacturen => _facturenViewModel.Facturen.Count;
        public int TotaalAantalOffertes => _offertenViewModel.Offertes.Count;
        public decimal TotaleOmzet => _facturenViewModel.Facturen.Sum(f => f.Totaal);
        public decimal OpenstaandBedrag => _facturenViewModel.Facturen.Where(f => f.Status != "Betaald").Sum(f => f.Totaal);

        public DashboardViewModel(KlantenViewModel klantenViewModel, FacturenViewModel facturenViewModel, OffertenViewModel offertenViewModel)
        {
            _klantenViewModel = klantenViewModel;
            _facturenViewModel = facturenViewModel;
            _offertenViewModel = offertenViewModel;

            _klantenViewModel.PropertyChanged += (s, e) => OnPropertyChanged(nameof(TotaalAantalKlanten));
            _facturenViewModel.PropertyChanged += (s, e) => 
            {
                OnPropertyChanged(nameof(TotaalAantalFacturen));
                OnPropertyChanged(nameof(TotaleOmzet));
                OnPropertyChanged(nameof(OpenstaandBedrag));
            };
            _offertenViewModel.PropertyChanged += (s, e) => OnPropertyChanged(nameof(TotaalAantalOffertes));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
