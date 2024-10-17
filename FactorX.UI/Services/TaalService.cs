using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace FactorX.UI.Services
{
    public class TaalService
    {
        public event EventHandler TaalGewijzigd;

        public void WijzigTaal(string taalCode)
        {
            if (taalCode != Thread.CurrentThread.CurrentUICulture.Name)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(taalCode);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(taalCode);

                Application.Current.Resources.MergedDictionaries.Clear();
                ResourceDictionary dict = new ResourceDictionary();
                switch (taalCode)
                {
                    case "en":
                        dict.Source = new Uri("..\\Resources\\Strings.en.xaml", UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("..\\Resources\\Strings.xaml", UriKind.Relative);
                        break;
                }
                Application.Current.Resources.MergedDictionaries.Add(dict);

                TaalGewijzigd?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
