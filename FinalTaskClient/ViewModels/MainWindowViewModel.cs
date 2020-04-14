using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MVVMCore.Helpers;

namespace FinalTaskClient.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<Person> Persons { get; set; }
        
        private int _languageId = 0;
        private int _pageIndex = 0;

        public int LanguageId
        {
            get=>_languageId;
            set
            {
                if (!(_languageId.Equals(value)))
                {
                    _languageId = value;
                    switch (_languageId)
                    {
                        case 0:
                            var rd0 = new ResourceDictionary();
                            var uri0 = "Views/Resources/GermanLocalization.xaml";
                            rd0.MergedDictionaries.Add(App.LoadComponent(new Uri(uri0, UriKind.Relative)) as ResourceDictionary);
                            Application.Current.Resources = rd0;
                            break;
                        case 1:
                            var rd1 = new ResourceDictionary();
                            var uri1 = "Views/Resources/FrenchLocalization.xaml";
                            rd1.MergedDictionaries.Add(App.LoadComponent(new Uri(uri1, UriKind.Relative)) as ResourceDictionary);
                            Application.Current.Resources = rd1;
                            break;
                        case 2:
                            var rd2 = new ResourceDictionary();
                            var uri2 = "Views/Resources/ItalianLocalization.xaml";
                            rd2.MergedDictionaries.Add(App.LoadComponent(new Uri(uri2, UriKind.Relative)) as ResourceDictionary);
                            Application.Current.Resources = rd2;
                            break;
                        case 3:
                            var rd3 = new ResourceDictionary();
                            var uri3 = "Views/Resources/EnglishLocalization.xaml";
                            rd3.MergedDictionaries.Add(App.LoadComponent(new Uri(uri3, UriKind.Relative)) as ResourceDictionary);
                            Application.Current.Resources = rd3;
                            break;
                    }
                }
            }
        }

        public int PageIndex
        {
            get => _pageIndex;
            set { _pageIndex = value; }
        }

        
        public DelegateCommand NewPersonCommand { get; set; }
        public DelegateCommand EditPersonCommand { get; set; }
        public DelegateCommand DeletePersonCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand NextPageCommand { get; set; }
        public DelegateCommand PrevPageCommand { get; set; }


        


        public MainWindowViewModel()
        {
            

            ClientWorker.LoadCountries();
            ClientWorker.LoadGreetings();
            ClientWorker.LoadPersons();
            ClientWorker.LoadPersonContacts();
            Persons = new ObservableCollection<Person>();
            NewPersonCommand=new DelegateCommand(OnNewPerson);
            EditPersonCommand=new DelegateCommand(OnEditPerson, CanEditPerson);
            DeletePersonCommand = new DelegateCommand(OnDeletePerson, CanDeletePerson);
            UpdateCommand=new DelegateCommand(OnUpdate);
            NextPageCommand = new DelegateCommand(OnNextPage);
            PrevPageCommand = new DelegateCommand(OnPrevCommand);
            

        }

        private void OnPrevCommand(object obj)
        {
            if (_pageIndex - 1 >= 0)
            {
                _pageIndex--;
                LoadPersons();
            }
        }

        private void OnNextPage(object obj)
        {
            int pages = ClientWorker.Persons.Count / 100;
            if (ClientWorker.Persons.Count % 100 != 0) pages++;
            if (_pageIndex + 1 <= pages)
            {
                _pageIndex++;
                LoadPersons();
            }
        }

        private void LoadPersons()
        {
            
            Persons.Clear();
            for (int i = _pageIndex * 100; i < (_pageIndex + 1) * 100; i++)
            {
                if(i>=ClientWorker.Persons.Count) break;
                Persons.Add(ClientWorker.Persons[i]);
            }

        }
        

        private void OnUpdate(object obj)
        {
            ClientWorker.LoadPersons();
            LoadPersons();
        }

        private bool CanDeletePerson(object obj)
        {
            if (!(obj is DataGrid dtGr)) return false;
            return dtGr.SelectedCells.Count > 0;
        }

        private async void OnDeletePerson(object obj)
        {
            var dtGr = obj as DataGrid;
            foreach (var cell in dtGr.SelectedCells)
            {
                ClientWorker.Persons.Remove(cell.Item as Person);
                if (await ClientWorker.DeletePerson(cell.Item as Person))
                {
                    MessageBox.Show("Person was deleted successfully!");
                }
                else
                {
                    MessageBox.Show("Person wasn't deleted successfully!");

                }
            }
        }

        private bool CanEditPerson(object obj)
        {
            if (!(obj is DataGrid dtGr)) return false;
            return dtGr.SelectedCells.Count/13 == 1;
        }

        private async void OnEditPerson(object obj)
        {
            var person = CopyValueHelper.CreateNewAndCopy<Person, Person>((obj as DataGrid).SelectedItem as Person, typeof(Person));
            var view = new NewEditWindowView(person);

            try
            {
                var res = (bool)view.ShowDialog();
                if (res)
                {
                    if ( await ClientWorker.UpdatePerson(person))
                    {
                        CopyValueHelper.CopyValues(person, (obj as DataGrid).SelectedItem as Person);
                        MessageBox.Show("Person was updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Person was updated unsuccessfully!");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void OnNewPerson(object obj)
        {
            var person = new Person();
            var view = new NewEditWindowView(person);

            try
            {
                var res = (bool)view.ShowDialog();
                if (res)
                {
                    ClientWorker.Persons.Add(person);
                    if (await ClientWorker.AddPerson(person))
                    {

                        MessageBox.Show("New Person was added successfully!");
                    }
                    else
                    {
                        MessageBox.Show("New Person wasn't added successfully!");

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
