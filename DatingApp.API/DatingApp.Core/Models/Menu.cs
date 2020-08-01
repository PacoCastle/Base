using System;

namespace DatingApp.Core.Models
{
    public class Menu
    {
        public int Id { get; set; }
        
        public string path { get; set; }

        public string title { get; set; }

        public string icon { get; set; }

        public int ParentId { get; set; }    

        public int Status { get; set; }    

        
        
    }
}