using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.CommonFeatures.Commands
{
    public abstract class UpdateUserCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Polis { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string? Sex { get; set; }
    }
}
