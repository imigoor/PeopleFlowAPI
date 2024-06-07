using PeopleHubAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace PeopleHubAPI.InfraestruturaBD
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(People people)
        {
            _context.Peoples.Add(people);
            _context.SaveChanges();
        }

        public List<People> Get(int pageNumber, int pageQuantity)
        {
            return _context.Peoples.Skip(pageNumber * pageQuantity).Take(pageQuantity).ToList();
        }

        public People? Get(int id)
        {
            return _context.Peoples.Find(id);
        }
    }
}