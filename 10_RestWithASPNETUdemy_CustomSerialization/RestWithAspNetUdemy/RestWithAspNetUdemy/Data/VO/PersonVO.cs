using System.Text.Json.Serialization;

namespace RestWithAspNetUdemy.Model
{
    public class PersonVO
    {
        [JsonPropertyName("Codigo")]
        public long Id { get; set; }

        [JsonPropertyName("Nome")]
        public string Name { get; set; }

        [JsonIgnore]
        public string Adress { get; set; }

        [JsonPropertyName("Genero")]
        public string Gender { get; set; }
    }
}
