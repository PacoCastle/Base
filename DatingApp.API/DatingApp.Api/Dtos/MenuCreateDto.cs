namespace DatingApp.Api.Dtos
{
    public class MenuCreateDto
    {
        public string Path { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public int ParentId { get; set; }    

        public int Status { get; set; }        
    }
}