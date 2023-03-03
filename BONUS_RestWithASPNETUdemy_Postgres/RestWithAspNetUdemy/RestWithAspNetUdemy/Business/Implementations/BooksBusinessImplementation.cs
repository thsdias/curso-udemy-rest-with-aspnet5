using RestWithAspNetUdemy.Data.Converter.Implementations;
using RestWithAspNetUdemy.Model;
using RestWithAspNetUdemy.Repository;
using System.Collections.Generic;

namespace RestWithAspNetUdemy.Business.Implementations
{
    public class BooksBusinessImplementation : IBooksBusiness
    {
        private readonly IRepository<Books> _repository;
        private readonly BooksConverter _converter;

        public BooksBusinessImplementation(IRepository<Books> repository) 
        {
            _repository = repository;
            _converter = new BooksConverter();
        }

        public List<BooksVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BooksVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BooksVO Create(BooksVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);

            return _converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public BooksVO Update(BooksVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);

            return _converter.Parse(bookEntity);
        }
    }
}
