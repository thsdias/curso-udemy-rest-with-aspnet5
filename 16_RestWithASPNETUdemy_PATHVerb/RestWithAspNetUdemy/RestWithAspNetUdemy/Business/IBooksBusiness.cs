using RestWithAspNetUdemy.Model;
using System.Collections.Generic;

namespace RestWithAspNetUdemy.Business
{
    public interface IBooksBusiness
    {
        BooksVO Create(BooksVO Books);

        BooksVO FindById(long id);

        List<BooksVO> FindAll();

        BooksVO Update(BooksVO Books);

        void Delete(long id);
    }
}
