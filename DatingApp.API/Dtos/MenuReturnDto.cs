namespace DatingApp.API.Dtos
{
    public class MenuReturnDto
    {
        public int Id { get; set; }
        
        public string Path { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public int ParentId { get; set; }    

        public int Status { get; set; }    
    }
}