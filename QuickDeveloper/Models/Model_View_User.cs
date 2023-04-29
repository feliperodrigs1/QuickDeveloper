using System.ComponentModel.DataAnnotations;

namespace QuickDeveloper.Models
{
    public class Model_View_User
    {
        public int Id { get; set; }
        public string Username { get; set; }       
        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }       
        public string RoleName { get; set; }
        public string Competences { get; set; }
        public string AditionalInfo { get; set; }
    }
}
