using System.Text.Json.Serialization;

namespace MedBratAPI.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Clinic> Clinics { get; set; } = new List<Clinic>();
    }
}
