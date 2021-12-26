using System.Collections.Generic;
using trippicker_api.Entities;

namespace trippicker_api.Models.Account
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
