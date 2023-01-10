using System;

namespace RestWithAspNetUdemy.Model
{
    public class BooksVO
    {
        public long Id { get; set; }

        public String Author { get; set; }

        public DateTime LaunchDate { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }
    }
}
