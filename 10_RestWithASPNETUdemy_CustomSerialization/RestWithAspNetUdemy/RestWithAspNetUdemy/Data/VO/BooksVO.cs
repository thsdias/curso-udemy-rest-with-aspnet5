using System;
using System.Text.Json.Serialization;

namespace RestWithAspNetUdemy.Model
{
    public class BooksVO
    {
        [JsonPropertyName("Codigo")]
        public long Id { get; set; }

        [JsonPropertyName("Autor")]
        public String Author { get; set; }

        [JsonPropertyName("Data Publicação")]
        public DateTime LaunchDate { get; set; }

        [JsonPropertyName("Preço")]
        public decimal Price { get; set; }

        [JsonPropertyName("Titulo")]
        public string Title { get; set; }
    }
}
