using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FactorX.Core.Models;
using FactorX.UI.Views;

namespace FactorX.UI.ViewModels
{
    public class FacturenViewModel : INotifyPropertyChanged
    {
        private readonly ProductViewModel _productViewModel;
        private readonly BetalingService _betalingService;

        public ObservableCollection<Product> Producten => _productViewModel.Producten;

        public ICommand RegistreerBetalingCommand { get; private set; }

        public FacturenViewModel(ProductViewModel productViewModel, BetalingService betalingService)
        {
            _productViewModel = productViewModel;
            _betalingService = betalingService;
            RegistreerBetalingCommand = new RelayCommand<Factuur>(RegistreerBetaling);
            // ... bestaande code ...
        }

        // ... bestaande code ...

        public void ProductGeselecteerd(FactuurRegel regel, Product product)
        {
            if (product != null)
            {
                regel.Product = product;
                regel.Omschrijving = product.Beschrijving;
                regel.Prijs = product.Prijs;
                // Trigger PropertyChanged voor de FactuurRegel properties
            }
        }

        private void RegistreerBetaling(Factuur factuur)
        {
            if (factuur != null)
            {
                var betalingView = new BetalingView(factuur);
                if (betalingView.ShowDialog() == true)
                {
                    try
                    {
                        _betalingService.RegistreerBetaling(factuur, betalingView.BetaaldBedrag, betalingView.BetaaldOp);
                        // Trigger PropertyChanged voor relevante eigenschappen
                        OnPropertyChanged(nameof(Facturen));
                    }
                    catch (ArgumentException ex)
                    {
                        // Toon foutmelding aan gebruiker
                        MessageBox.Show(ex.Message, "Fout bij registreren betaling", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
