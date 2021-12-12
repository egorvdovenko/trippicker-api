using System.Collections.Generic;
using demo_web_api.Entities;

namespace demo_web_api.Models.Account
{
    public class UserModel
    {
        public UserModel()
        { }

        public UserModel(UserEntity user)
        {
            Id = user.Id;
            Email = user.Email;
            Confirmed = user.Confirmed;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public bool Confirmed { get; set; }
    }
}
