using RestWithAspNetUdemy.Data.Converter.Contract;
using RestWithAspNetUdemy.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNetUdemy.Data.Converter.Implementations
{
    public class PersonConverter : IParse<PersonVO, Person>, IParse<Person, PersonVO>
    {
        public Person Parse(PersonVO origin)
        {
            if(origin == null) return null;

            var nome = origin.Name.Split(" ");

            return new Person
            {
                Id = origin.Id,
                FirstName = nome[0],
                LastName = nome[1],
                Adress = origin.Adress,
                Gender = origin.Gender
            };
        }

        public PersonVO Parse(Person origin)
        {
            if (origin == null) return null;

            return new PersonVO
            {
                Id = origin.Id,
                Name = string.Format("{0} {1}", origin.FirstName, origin.LastName),
                Adress = origin.Adress,
                Gender = origin.Gender
            };
        }

        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }

        public List<PersonVO> Parse(List<Person> origin)
        {
            if( origin == null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
