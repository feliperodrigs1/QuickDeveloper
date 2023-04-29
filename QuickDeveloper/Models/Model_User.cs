namespace QuickDeveloper.Models {
    public class Model_User {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Competences { get; set; }
        public string AditionalInfo { get; set; }
    }
}
