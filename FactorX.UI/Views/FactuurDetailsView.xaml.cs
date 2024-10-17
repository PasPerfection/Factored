using System.Windows;
using FactorX.UI.ViewModels;

namespace FactorX.UI.Views
{
    public partial class FactuurDetailsView : Window
    {
        public FactuurDetailsView(FacturenViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
