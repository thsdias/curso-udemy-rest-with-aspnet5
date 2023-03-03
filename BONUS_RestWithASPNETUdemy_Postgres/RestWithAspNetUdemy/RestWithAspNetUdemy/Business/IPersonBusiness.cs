using RestWithAspNetUdemy.Hypermedia.Utils;
using RestWithAspNetUdemy.Model;
using System.Collections.Generic;

namespace RestWithAspNetUdemy.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindById(long id);

        List<PersonVO> FindByName(string firstName, string lastName);

        PersonVO Update(PersonVO person);

        PersonVO Disable(long id);

        void Delete(long id);

        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
    }
}
