using RestWithAspNetUdemy.Hypermedia;
using RestWithAspNetUdemy.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWithAspNetUdemy.Model
{
    public class PersonVO : ISupportsHyperMedia
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }
        
        public string Gender { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
