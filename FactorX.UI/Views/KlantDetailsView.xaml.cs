using System.Windows;
using FactorX.UI.ViewModels;

namespace FactorX.UI.Views
{
    public partial class KlantDetailsView : Window
    {
        public KlantDetailsView(KlantenViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
