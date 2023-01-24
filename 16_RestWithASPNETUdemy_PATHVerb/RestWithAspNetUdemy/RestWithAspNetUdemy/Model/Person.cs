using RestWithAspNetUdemy.Business.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNetUdemy.Model
{
    [Table("person")]
    public class Person : BaseEntity
    {
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName{ get; set; }

        [Column("address")]
        public string Adress { get; set; }
        
        [Column("gender")]
        public string Gender { get; set; } 
        
        [Column("enabled")]
        public bool Enabled { get; set; }
    }
}
