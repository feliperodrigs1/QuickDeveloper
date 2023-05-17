using System.ComponentModel.DataAnnotations;

namespace QuickDeveloper.Models 
{
    public class Model_User 
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Birthdate { get; set; }
        public string Role { get; set; }
        public string RoleName { get; set; }
        public string Competences { get; set; }
        public string AditionalInfo { get; set; }
    }
}
