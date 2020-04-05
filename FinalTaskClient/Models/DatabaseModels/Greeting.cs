using System.Collections.Generic;

namespace FinalTaskClient.Models.DatabaseModels
{
    public partial class Greeting
    {
        public Greeting()
        {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Txt1 { get; set; }
        public string Txt2 { get; set; }
        public string Txt3 { get; set; }
        public string Txt4 { get; set; }
        public string TxtLetter1 { get; set; }
        public string TxtLetter2 { get; set; }
        public string TxtLetter3 { get; set; }
        public string TxtLetter4 { get; set; }
        public sbyte Active { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
