using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNetUdemy.Model
{
    [Table("books")]
    public class Books
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("author")]
        public String Author { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("title")]
        public string Title { get; set; }
    }
}
