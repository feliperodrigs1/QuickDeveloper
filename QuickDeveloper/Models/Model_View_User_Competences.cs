using System.ComponentModel.DataAnnotations;

namespace QuickDeveloper.Models
{
    public class Model_View_User_Competences
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Competences { get; set; }
        public string AditionalInfo { get; set; }
        public int Burden { get; set; }
    }
}
