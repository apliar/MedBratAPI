using System.Text.Json.Serialization;

namespace MedBratAPI.Models
{
    public class Curator : User
    {
        public int? ClinicId { get; set; }
        [JsonIgnore]
        public Clinic Clinic { get; set; }
    }
}
