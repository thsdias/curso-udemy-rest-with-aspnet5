using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNetUdemy.Business.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
