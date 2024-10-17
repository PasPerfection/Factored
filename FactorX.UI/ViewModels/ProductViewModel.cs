using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using FactorX.Core.Models;
using FactorX.UI.Services;

namespace FactorX.UI.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly ZoekService _zoekService;
        private ObservableCollection<Product> _producten;
        private Product _geselecteerdProduct;
        private Product _huidigProduct;

        public ObservableCollection<Product> Producten
        {
            get => _producten;
            set
            {
                _producten = value;
                OnPropertyChanged(nameof(Producten));
            }
        }

        public Product GeselecteerdProduct
        {
            get => _geselecteerdProduct;
            set
            {
                _geselecteerdProduct = value;
                OnPropertyChanged(nameof(GeselecteerdProduct));
            }
        }

        public Product HuidigProduct
        {
            get => _huidigProduct;
            set
            {
                _huidigProduct = value;
                OnPropertyChanged(nameof(HuidigProduct));
            }
        }

        public ICommand NieuwProductCommand { get; private set; }
        public ICommand BewerkenCommand { get; private set; }
        public ICommand VerwijderenCommand { get; private set; }
        public ICommand OpslaanCommand { get; private set; }
        public ICommand AnnulerenCommand { get; private set; }

        public ProductViewModel(ZoekService zoekService)
        {
            _zoekService = zoekService;
            Producten = new ObservableCollection<Product>();
            NieuwProductCommand = new RelayCommand(MaakNieuwProduct);
            BewerkenCommand = new RelayCommand(BewerkProduct, CanBewerkProduct);
            VerwijderenCommand = new RelayCommand(VerwijderProduct, CanVerwijderProduct);
            OpslaanCommand = new RelayCommand(SlaProductOp, CanSlaProductOp);
            AnnulerenCommand = new RelayCommand(AnnuleerBewerking, CanAnnuleerBewerking);
        }

        private void MaakNieuwProduct()
        {
            HuidigProduct = new Product();
            OpenProductDetailsView();
        }

        private void BewerkProduct()
        {
            if (GeselecteerdProduct != null)
            {
                HuidigProduct = new Product
                {
                    Id = GeselecteerdProduct.Id,
                    Naam = GeselecteerdProduct.Naam,
                    Beschrijving = GeselecteerdProduct.Beschrijving,
                    Prijs = GeselecteerdProduct.Prijs,
                    Eenheid = GeselecteerdProduct.Eenheid,
                    BTWPercentage = GeselecteerdProduct.BTWPercentage
                };
                OpenProductDetailsView();
            }
        }

        private bool CanBewerkProduct() => GeselecteerdProduct != null;

        private void VerwijderProduct()
        {
            if (GeselecteerdProduct != null)
            {
                Producten.Remove(GeselecteerdProduct);
            }
        }

        private bool CanVerwijderProduct() => GeselecteerdProduct != null;

        private void SlaProductOp()
        {
            if (HuidigProduct.Id == 0)
            {
                HuidigProduct.Id = Producten.Count + 1;
                Producten.Add(HuidigProduct);
            }
            else
            {
                var bestaandProduct = Producten.FirstOrDefault(p => p.Id == HuidigProduct.Id);
                if (bestaandProduct != null)
                {
                    int index = Producten.IndexOf(bestaandProduct);
                    Producten[index] = HuidigProduct;
                }
            }
            HuidigProduct = null;
            CloseProductDetailsView();
        }

        private bool CanSlaProductOp() => HuidigProduct != null;

        private void AnnuleerBewerking()
        {
            HuidigProduct = null;
            CloseProductDetailsView();
        }

        private bool CanAnnuleerBewerking() => HuidigProduct != null;

        private void OpenProductDetailsView()
        {
            // Implementeer deze methode om ProductDetailsView te openen
        }

        private void CloseProductDetailsView()
        {
            // Implementeer deze methode om ProductDetailsView te sluiten
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
