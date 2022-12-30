using RestWithAspNetUdemy.Model;
using System.Collections.Generic;

namespace RestWithAspNetUdemy.Repository
{
    public interface IBooksRepository
    {
        List<Books> FindAll();

        Books FindById(long id);

        Books Create(Books books);

        Books Update(Books books);
        
        void Delete(long id);
        
        bool Exists(long id);
    }
}
