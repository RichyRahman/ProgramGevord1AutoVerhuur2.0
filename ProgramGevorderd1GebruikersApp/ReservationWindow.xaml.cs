using Autoverhuur.Domain.Models;
using ProgramGevorderd1GebruikersApp.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProgramGevorderd1GebruikersApp
{
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class ReservationWindow : Window
    {
        private List<Customer> _customers = new();
        private List<Establishment> _locations = new();
        private List<Car> _availableCars = new();
        public ReservationWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // TODO: Ophalen uit databank of mock
            _customers = MockDatabase.GetCustomers();
            _locations = MockDatabase.GetEstablishments();

            CustomerListBox.ItemsSource = _customers;
            LocationComboBox.ItemsSource = _locations;
        }

        private void SearchCars_Click(object sender, RoutedEventArgs e)
        {
            if (LocationComboBox.SelectedItem is not Establishment location ||
                StartDatePicker.SelectedDate is not DateTime start ||
                EndDatePicker.SelectedDate is not DateTime end || start >= end)
            {
                MessageBox.Show("Vul alle filters correct in.");
                return;
            }

            int.TryParse(SeatsTextBox.Text, out int seats);
            _availableCars = MockDatabase.SearchAvailableCars(location, start, end, seats);
            AvailableCarsListBox.ItemsSource = _availableCars;
        }

        private void ConfirmReservation_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerListBox.SelectedItem is not Customer customer ||
                AvailableCarsListBox.SelectedItem is not Car car ||
                StartDatePicker.SelectedDate is not DateTime start ||
                EndDatePicker.SelectedDate is not DateTime end)
            {
                MessageBox.Show("Selecteer een klant, auto en periode.");
                return;
            }

            var reservation = new Reservation
            {
                Customer = customer,
                Car = car,
                StartDate = start,
                EndDate = end
            };

            MockDatabase.SaveReservation(reservation);
            MessageBox.Show("Reservatie succesvol aangemaakt.");
        }
    }
}
