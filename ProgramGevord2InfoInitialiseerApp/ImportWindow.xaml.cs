using Microsoft.Win32;
using ProgramGevord1InfoInitialiseerApp.Controller;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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

namespace ProgramGevord1InfoInitialiseerApp
{
    /// <summary>
    /// Interaction logic for ImportWindow.xaml
    /// </summary>
    public partial class ImportWindow : Window
    {
        private readonly DataImportController _dataImportController;
        public ImportWindow(DataImportController dataImportController)
        {
            InitializeComponent();
            _dataImportController = dataImportController;
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            var estPath = PromptFile("Selecteer Establishments.csv");
            var custPath = PromptFile("Selecteer Customers.csv");
            var carPath = PromptFile("Selecteer Cars.csv");

            if (estPath == null || custPath == null || carPath == null)
            {
                MessageBox.Show("Alle bestanden moeten geselecteerd worden.");
                return;
            }

            var summary = _dataImportController.ImportAll(estPath, custPath, carPath, "errors.csv");
            MessageBox.Show($"Vestigingen: {summary.EstablishmentsImported}\nKlanten: {summary.CustomersImported}\nAuto's: {summary.CarsImported}\nFouten: {summary.ErrorCount}");
        }

        private string? PromptFile(string title)
        {
            var dlg = new OpenFileDialog
            {
                Title = title,
                Filter = "CSV files (*.csv)|*.csv"
            };
            return dlg.ShowDialog() == true ? dlg.FileName : null;
        }

        private string? establishmentPath, customerPath, carPath;

        private void SelectEstablishmentFile(object sender, RoutedEventArgs e)
        {
            establishmentPath = PromptFile("Selecteer Establishments.csv");
        }

        private void SelectCustomerFile(object sender, RoutedEventArgs e)
        {
            customerPath = PromptFile("Selecteer Customers.csv");
        }

        private void SelectCarFile(object sender, RoutedEventArgs e)
        {
            carPath = PromptFile("Selecteer Cars.csv");
        }
    }
}
