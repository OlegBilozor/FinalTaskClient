using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommonMVVM.Common;
using CommonMVVM.ViewModel;
using FinalTaskClient.Models.DatabaseModels;
using FinalTaskClient.Views;

namespace FinalTaskClient.ViewModels
{
    public class NewEditContactWindowViewModel : BaseViewModel
    {
        private readonly PersonContact _personContact;
        public int ContactTypeId { get=>_personContact.ContactTypeId; set=>_personContact.ContactTypeId=value; }
        public string Txt { get; set; }
        /// <summary>
        /// OK
        /// </summary>
        public DelegateCommand SaveChangesCommand { get; set; }
        /// <summary>
        /// Cancel
        /// </summary>
        public DelegateCommand CancelChangesCommand { get; set; }

        public NewEditContactWindowViewModel(PersonContact person)
        {
            _personContact = person;
            SaveChangesCommand = new DelegateCommand(p=>{ }, CanSaveChanges);
            CancelChangesCommand = new DelegateCommand(p => { });
        }

        

        private bool CanSaveChanges(object obj)
        {
            return !string.IsNullOrWhiteSpace(Txt);
        }

        
    }
}
