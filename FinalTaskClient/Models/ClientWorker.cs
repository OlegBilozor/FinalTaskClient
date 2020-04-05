using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FinalTaskClient.Models.DatabaseModels;

namespace FinalTaskClient.Models
{
    public  static class ClientWorker
    {
        private static HttpClient _client = new HttpClient();
        public static List<Person> Persons { get; set; }
        public static List<Country> Countries { get; set; }
        public  static List<Greeting> Greetings { get; set; }
        public static List<PersonContact> PersonContacts { get; set; }

        public static async void LoadPersons()
        {
           
            HttpResponseMessage response = await _client.GetAsync("http://localhost:44393/api/person");
            if (response.IsSuccessStatusCode)
            {
                Persons = await response.Content.ReadAsAsync<List<Person>>();
            }

            
        }
        public static async void LoadCountries()
        {
            
            HttpResponseMessage response = await _client.GetAsync("http://localhost:44393/api/country");
            if (response.IsSuccessStatusCode)
            {
                Countries = await response.Content.ReadAsAsync<List<Country>>();
            }

            
        }
        public static async void LoadGreetings()
        {
            
            HttpResponseMessage response = await _client.GetAsync("http://localhost:44393/api/greeting");
            if (response.IsSuccessStatusCode)
            {
                Greetings = await response.Content.ReadAsAsync<List<Greeting>>();
            }

           
        }
        public static async void LoadPersonContacts()
        {
            
            HttpResponseMessage response = await _client.GetAsync("http://localhost:44393/api/personContact");
            if (response.IsSuccessStatusCode)
            {
                PersonContacts = await response.Content.ReadAsAsync<List<PersonContact>>();
            }

            
        }
        public static async Task<List<PersonContact>> LoadPersonContacts(int personId)
        {
            List<PersonContact> contacts = null;
            HttpResponseMessage response = await _client.GetAsync($"http://localhost:44393/api/personContact/{personId}");
            if (response.IsSuccessStatusCode)
            {
                contacts = await response.Content.ReadAsAsync<List<PersonContact>>();
            }

            return contacts;
        }

        public static async Task<bool> AddPerson(Person person)
        {

            HttpResponseMessage response = await _client.PostAsJsonAsync("http://localhost:44393/api/person", person);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
        public static async Task<bool> AddPersonContact(PersonContact contact)
        {

            HttpResponseMessage response = await _client.PostAsJsonAsync("http://localhost:44393/api/personContact", contact);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public static async Task<bool> DeletePerson(Person person)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"http://localhost:44393/api/person/{person.Id}" );
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
        public static async Task<bool> DeletePersonContact(PersonContact contact)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"http://localhost:44393/api/person/{contact.PersonContactId}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public static async Task<bool> UpdatePerson(Person person)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"http://localhost:44393/api/person/{person.Id}", person);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
        public static async Task<bool> UpdatePersonContact(PersonContact contact)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"http://localhost:44393/api/person/{contact.PersonContactId}", contact);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
