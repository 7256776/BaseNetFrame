using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.WebApi
{
    public class AuthenticateModel
    {
        [Required]
        [StringLength(255)]
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        public bool RememberClient { get; set; }
    }

}
