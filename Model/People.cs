using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleHubAPI.Model
{
    [Table("people")]
    public class People
    {
        [Key]
        public int id {  get; private set; }
        public string name { get; private set; }
        public int age {  get; private set; }
        public string photo {  get; private set; }

        public People()
        {

        }

        public People(int id, string name, int age, string photo)
        {
            this.id = id;
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.age = age;
            this.photo = photo;
        }
    }
}