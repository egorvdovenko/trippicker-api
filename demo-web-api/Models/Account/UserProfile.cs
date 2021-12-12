using demo_web_api.Entities;

namespace demo_web_api.Models.Account
{
    public class UserProfile
    {
        public UserProfile()
        { }

        public UserProfile(UserEntity user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }
}
