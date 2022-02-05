namespace Starware.DatingApp.Core.Domains
{
    public class AppUser : Entity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; } 
        public DateTime BirthDate { get; set; }
        public Byte[] PasswordHash { get; set; }
        public Byte[] PasswordSalt  { get; set; }


    }
}