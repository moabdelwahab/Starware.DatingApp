namespace Starware.DatingApp.Core.Domains
{
    public class AppUser : Entity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }        

    }
}