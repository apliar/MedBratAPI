using System.Text.Json.Serialization;

namespace MedBratAPI.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }
        [JsonIgnore]
        public Doctor? Doctor { get; set; }

        public Dictionary<int, List<TimeSpan>> WeekSchedule { get; set; } 
            = new Dictionary<int, List<TimeSpan>>();
    }
}
