using System;

namespace OneCasa.Models.ViewModels
{
    //upcoming and past events model
    public class Events
    {
        public string EventName { get; set; }
        
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        
        public DateTime Date { get; set; }
    }
}