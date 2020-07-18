using System;

namespace DatingApp.Core
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public int Status { get; set; }        

        public string urlPhoto { get; set; }        
    }
}