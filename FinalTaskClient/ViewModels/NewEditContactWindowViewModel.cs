using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonMVVM.Common;
using CommonMVVM.ViewModel;
using FinalTaskClient.Models.DatabaseModels;

namespace FinalTaskClient.ViewModels
{
    class NewEditContactWindowViewModel : BaseViewModel
    {
        private readonly PersonContact _personContact;
        public int ContactTypeId { get=>_personContact.ContactTypeId; set=>_personContact.ContactTypeId=value; }
        public string Txt { get; set; }
        public DelegateCommand SaveChanges { get; set; }
        public DelegateCommand CancelChanges { get; set; }

        public NewEditContactWindowViewModel(PersonContact person)
        {
            _personContact = person;
            SaveChanges = new DelegateCommand(p => { }, CanSaveChanges);
            CancelChanges = new DelegateCommand(p => { });
        }

        private bool CanSaveChanges(object obj)
        {
            return !string.IsNullOrWhiteSpace(Txt);
        }

        
    }
}
