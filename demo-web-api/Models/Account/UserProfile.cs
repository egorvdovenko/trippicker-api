using trippicker_api.Entities;

namespace trippicker_api.Models.Account
{
    public class UserProfile
    {
        public UserProfile()
        { }

        public UserProfile(UserEntity user)
        {
            Id = user.Id;
            Email = user.Email;
        }

        public int Id { get; set; }
        public string Email { get; set; }
    }
}
