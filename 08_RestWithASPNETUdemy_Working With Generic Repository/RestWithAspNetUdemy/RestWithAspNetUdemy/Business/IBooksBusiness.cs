using RestWithAspNetUdemy.Model;
using System.Collections.Generic;

namespace RestWithAspNetUdemy.Business
{
    public interface IBooksBusiness
    {
        Books Create(Books Books);

        Books FindById(long id);

        List<Books> FindAll();

        Books Update(Books Books);

        void Delete(long id);
    }
}
