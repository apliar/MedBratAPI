using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.CommonFeatures.Commands
{
    public abstract class CreateUserCommand
    {
        [JsonRequired]
        public string Name { get; set; }
        [JsonRequired]
        public string Polis { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [JsonRequired]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string? Sex { get; set; }
        [JsonRequired]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
