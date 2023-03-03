using RestWithAspNetUdemy.Model;
using System.Collections.Generic;

namespace RestWithAspNetUdemy.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);

        List<Person> FindByName(string firstName, string secondName);
    }
}
