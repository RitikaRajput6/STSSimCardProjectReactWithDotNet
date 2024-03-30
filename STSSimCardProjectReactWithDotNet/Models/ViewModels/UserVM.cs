using System.ComponentModel.DataAnnotations;

namespace STSSimCardProjectReactWithDotNet.Models.ViewModels
{
    public class UserVM
    {

        public int Id { get; set; }
        [Required]
        public string EMail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
