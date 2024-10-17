using FactorX.UI.Services;
using FactorX.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace FactorX.UI
{
    public partial class MainWindow : Window
    {
        private readonly TaalService _taalService;
        private readonly ZoekService _zoekService;
        private readonly RapportService _rapportService;
        private readonly KlantenViewModel _klantenViewModel;
        private readonly FacturenViewModel _facturenViewModel;
        private readonly OffertenViewModel _offertenViewModel;
        private readonly ProductViewModel _productViewModel;
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly RapportViewModel _rapportViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _taalService = new TaalService();
            _zoekService = new ZoekService();
            _rapportService = new RapportService();
            _klantenViewModel = new KlantenViewModel(_zoekService);
            _facturenViewModel = new FacturenViewModel(_zoekService, _productViewModel);
            _offertenViewModel = new OffertenViewModel(_zoekService, _productViewModel);
            _productViewModel = new ProductViewModel(_zoekService);
            _dashboardViewModel = new DashboardViewModel(_klantenViewModel, _facturenViewModel, _offertenViewModel);
            _rapportViewModel = new RapportViewModel(_rapportService, _facturenViewModel);

            DataContext = this;
            Resources["DashboardVM"] = _dashboardViewModel;
            Resources["RapportVM"] = _rapportViewModel;
            _taalService.TaalGewijzigd += (s, e) => UpdateUITaal();
        }

        private void TaalKeuze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TaalKeuze.SelectedItem is ComboBoxItem selectedItem)
            {
                string taalCode = selectedItem.Tag.ToString();
                _taalService.WijzigTaal(taalCode);
            }
        }

        private void ZoekTekst_TextChanged(object sender, TextChangedEventArgs e)
        {
            string zoekterm = ZoekTekst.Text;
            _klantenViewModel.ZoekKlanten(zoekterm);
            _facturenViewModel.ZoekFacturen(zoekterm);
            _offertenViewModel.ZoekOffertes(zoekterm);
        }

        private void UpdateUITaal()
        {
            // Update de taal van alle ViewModels
            _klantenViewModel.UpdateTaal();
            _facturenViewModel.UpdateTaal();
            _offertenViewModel.UpdateTaal();
        }
    }
}
