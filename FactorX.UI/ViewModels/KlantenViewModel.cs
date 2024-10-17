using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using FactorX.Core.Models;
using System.Linq;
using System;

namespace FactorX.UI.ViewModels
{
    public class KlantenViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Klant> _klanten;
        private Klant _geselecteerdeKlant;
        private Klant _huidigeKlant;

        public ObservableCollection<Klant> Klanten
        {
            get => _klanten;
            set
            {
                _klanten = value;
                OnPropertyChanged(nameof(Klanten));
            }
        }

        public Klant GeselecteerdeKlant
        {
            get => _geselecteerdeKlant;
            set
            {
                _geselecteerdeKlant = value;
                OnPropertyChanged(nameof(GeselecteerdeKlant));
            }
        }

        public Klant HuidigeKlant
        {
            get => _huidigeKlant;
            set
            {
                _huidigeKlant = value;
                OnPropertyChanged(nameof(HuidigeKlant));
            }
        }

        public ICommand ToevoegenCommand { get; private set; }
        public ICommand BewerkenCommand { get; private set; }
        public ICommand VerwijderenCommand { get; private set; }
        public ICommand OpslaanCommand { get; private set; }
        public ICommand AnnulerenCommand { get; private set; }

        public KlantenViewModel()
        {
            Klanten = new ObservableCollection<Klant>();
            ToevoegenCommand = new RelayCommand(VoegKlantToe, CanVoegKlantToe);
            BewerkenCommand = new RelayCommand(BewerkKlant, CanBewerkKlant);
            VerwijderenCommand = new RelayCommand(VerwijderKlant, CanVerwijderKlant);
            OpslaanCommand = new RelayCommand(SlaKlantOp, CanSlaKlantOp);
            AnnulerenCommand = new RelayCommand(AnnuleerBewerking, CanAnnuleerBewerking);
        }

        private void VoegKlantToe()
        {
            HuidigeKlant = new Klant { Id = Klanten.Count + 1, AanmaakDatum = DateTime.Now };
            // Open KlantDetailsView
        }

        private bool CanVoegKlantToe() => true;

        private void BewerkKlant()
        {
            if (GeselecteerdeKlant != null)
            {
                HuidigeKlant = new Klant
                {
                    Id = GeselecteerdeKlant.Id,
                    Naam = GeselecteerdeKlant.Naam,
                    Bedrijfsnaam = GeselecteerdeKlant.Bedrijfsnaam,
                    Email = GeselecteerdeKlant.Email,
                    Telefoonnummer = GeselecteerdeKlant.Telefoonnummer,
                    Adres = GeselecteerdeKlant.Adres,
                    Postcode = GeselecteerdeKlant.Postcode,
                    Plaats = GeselecteerdeKlant.Plaats,
                    BTWNummer = GeselecteerdeKlant.BTWNummer,
                    AanmaakDatum = GeselecteerdeKlant.AanmaakDatum
                };
                // Open KlantDetailsView
            }
        }

        private bool CanBewerkKlant() => GeselecteerdeKlant != null;

        private void VerwijderKlant()
        {
            if (GeselecteerdeKlant != null)
            {
                Klanten.Remove(GeselecteerdeKlant);
            }
        }

        private bool CanVerwijderKlant() => GeselecteerdeKlant != null;

        private void SlaKlantOp()
        {
            if (HuidigeKlant.Id == 0)
            {
                HuidigeKlant.Id = Klanten.Count + 1;
                Klanten.Add(HuidigeKlant);
            }
            else
            {
                var bestaandeKlant = Klanten.FirstOrDefault(k => k.Id == HuidigeKlant.Id);
                if (bestaandeKlant != null)
                {
                    int index = Klanten.IndexOf(bestaandeKlant);
                    Klanten[index] = HuidigeKlant;
                }
            }
            HuidigeKlant = null;
            // Sluit KlantDetailsView
        }

        private bool CanSlaKlantOp() => HuidigeKlant != null;

        private void AnnuleerBewerking()
        {
            HuidigeKlant = null;
            // Sluit KlantDetailsView
        }

        private bool CanAnnuleerBewerking() => HuidigeKlant != null;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();
    }
}
