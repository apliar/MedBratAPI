using System.Text.Json.Serialization;

namespace MedBratAPI.Models
{
    public class Patient : User
    {
        [JsonIgnore]
        public List<Doctor> Doctors { get; set; } = new();
        [JsonIgnore]
        public List<MedRecord> MedRecords { get; set; } = new();
        [JsonIgnore]
        public List<MedTicket> MedTickets { get; set; } = new();
    }
}
