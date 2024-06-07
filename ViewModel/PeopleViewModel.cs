using Microsoft.AspNetCore.Http;

namespace PeopleHubAPI.ViewModel
{
    public class PeopleViewModel
    {
        public int Id { get; set; }
       
        public string Name { get; set;}
        
        public int Age { get; set;}

        public IFormFile Photo { get; set; }
    }
}
