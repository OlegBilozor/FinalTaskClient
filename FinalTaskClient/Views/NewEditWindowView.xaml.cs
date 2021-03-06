﻿using System;
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
    /// Логика взаимодействия для NewEditWindowView.xaml
    /// </summary>
    public partial class NewEditWindowView
    {
        public NewEditWindowView(Person person)
        {
            InitializeComponent();
            var viewModel=new NewEditWindowViewModel(person);
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
