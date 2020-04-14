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
using FinalTaskClient.Models.DatabaseModels;
using FinalTaskClient.ViewModels;

namespace FinalTaskClient.Views
{
    /// <summary>
    /// Логика взаимодействия для NewEditContactWindowView.xaml
    /// </summary>
    public partial class NewEditContactWindowView
    {
        public NewEditContactWindowView(PersonContact contact)
        {
            InitializeComponent();
            var viewModel = new NewEditContactWindowViewModel(contact);
            viewModel.CancelChangesCommand.RequestCommand += Close;
            viewModel.SaveChangesCommand.RequestCommand += CloseCommandOnRequestCommand;
            DataContext = viewModel;
        }
        private void CloseCommandOnRequestCommand()
        {
            DialogResult = true;
            Close();
        }
    }
}
