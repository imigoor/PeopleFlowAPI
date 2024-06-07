namespace PeopleHubAPI.Model
{
    public interface IPeopleRepository
    {
        void Add (People people);

        List<People> Get(int pageNumber, int pageQuantity);

        People? Get(int id);
    }
}