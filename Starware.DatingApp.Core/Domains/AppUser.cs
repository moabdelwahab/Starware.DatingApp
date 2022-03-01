using Starware.DatingApp.SharedKernal.Utilities;

namespace Starware.DatingApp.Core.Domains
{
    public class AppUser : Entity
    {
        public AppUser()
        {
            LikedByUsers = new List<Like>();
            UserLikes = new List<Like>();

        }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; } 
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public Byte[] PasswordHash { get; set; }
        public Byte[] PasswordSalt  { get; set; }
        public string KnownAs { get; set; }
        public string Interests { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime LastActive { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public ICollection<Like> LikedByUsers { get; set; }
        public ICollection<Like> UserLikes { get; set; }


        public int GetAge()
        {
            return BirthDate.GetAgeFromDate();
        }

    }
}