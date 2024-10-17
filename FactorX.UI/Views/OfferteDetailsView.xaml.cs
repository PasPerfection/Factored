using System.Windows;
using FactorX.UI.ViewModels;

namespace FactorX.UI.Views
{
    public partial class OfferteDetailsView : Window
    {
        public OfferteDetailsView(OffertenViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
