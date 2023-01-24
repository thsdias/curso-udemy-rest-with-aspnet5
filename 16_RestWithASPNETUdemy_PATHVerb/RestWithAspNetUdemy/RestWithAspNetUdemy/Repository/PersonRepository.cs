using Microsoft.EntityFrameworkCore.Internal;
using RestWithAspNetUdemy.Model;
using RestWithAspNetUdemy.Model.Context;
using RestWithAspNetUdemy.Repository.Generic;
using System.Linq;

namespace RestWithAspNetUdemy.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySqlContext context) : base(context)
        {
        }

        public Person Disable(long id)
        {
            if(!_context.Persons.Any(p => p.Id.Equals(id)))
                return null;

            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if(user != null)
            {
                user.Enabled = false;

                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (System.Exception)
                {
                    throw;
                }
            }

            return user;
        }
    }
}
