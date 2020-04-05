using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CommonMVVM.Common;
using CommonMVVM.ViewModel;
using FinalTaskClient.Models;
using FinalTaskClient.Models.DatabaseModels;
using FinalTaskClient.Views;
using MVVMCore.Commands;
using MVVMCore.Helpers;

namespace FinalTaskClient.ViewModels
{
    class NewEditWindowViewModel: BaseViewModel
    {
        public List<Country> Countries {
            get => ClientWorker.Countries;
            set { }
        }
        public List<Greeting> Greetings {
            get => ClientWorker.Greetings;
            set{ }

        }
        public Person PersonToWorkWith { get; set; }
        public int LanguageId { get; set; } = 0;
        public DelegateCommand SaveChanges { get; set; }
        public DelegateCommand CancelChanges { get; set; }
        public DelegateCommand NewContactCommand { get; set; }
        public DelegateCommand EditContactCommand { get; set; }
        public DelegateCommand DeleteContactCommand { get; set; }

        public NewEditWindowViewModel(Person person)
        {
            PersonToWorkWith = person;
            SaveChanges=new DelegateCommand(p=>{ });
            CancelChanges=new DelegateCommand(p=>{ });
            NewContactCommand = new DelegateCommand(OnNewContact);
            EditContactCommand = new DelegateCommand(OnEditContact, CanEditContact);
            DeleteContactCommand = new DelegateCommand(OnDeleteContact, CanDeleteContact);
        }

        private bool CanDeleteContact(object obj)
        {
            if (!(obj is DataGrid dtGr)) return false;
            return dtGr.SelectedCells.Count > 1;
        }

        private async void OnDeleteContact(object obj)
        {
            var dtGr = obj as DataGrid;
            foreach (var cell in dtGr.SelectedCells)
            {
                PersonToWorkWith.PersonContact.Remove(cell.Item as PersonContact);
                if (await ClientWorker.DeletePersonContact(cell.Item as PersonContact))
                {
                    MessageBox.Show("PersonContact was deleted successfully!");
                }
                else
                {
                    MessageBox.Show("PersonContact wasn't deleted successfully!");

                }
            }
        }

        private bool CanEditContact(object obj)
        {
            if (!(obj is DataGrid dtGr)) return false;
            return dtGr.SelectedCells.Count == 1;
        }

        private async void OnEditContact(object obj)
        {
            var contact = CopyValueHelper.CreateNewAndCopy<PersonContact, PersonContact>((obj as DataGrid).SelectedItem as PersonContact, typeof(PersonContact));
            var view = new NewEditContactWindowView(contact);

            try
            {
                var res = (bool)view.ShowDialog();
                if (res)
                {
                    if (await ClientWorker.UpdatePersonContact(contact))
                    {
                        CopyValueHelper.CopyValues(contact, (obj as DataGrid).SelectedItem as PersonContact);
                        MessageBox.Show("Contact was updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Contact was updated unsuccessfully!");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void OnNewContact(object obj)
        {
            var contact = new PersonContact();
            var view = new NewEditContactWindowView(contact);

            try
            {
                var res = (bool)view.ShowDialog();
                if (res)
                {
                    PersonToWorkWith.PersonContact.Add(contact);
                    if (await ClientWorker.AddPersonContact(contact))
                    {

                        MessageBox.Show("New PersonContact was added successfully!");
                    }
                    else
                    {
                        MessageBox.Show("New PersonContact wasn't added successfully!");

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        

        

        

       
    }
}
