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
            FirstName = user.FirstName;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            Confirmed = user.Confirmed;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public bool Confirmed { get; set; }
        public List<int> RegionsIds { get; set; } = new List<int>();
        public List<int> ResponsiblesIds { get; set; } = new List<int>();
        public List<int> RolesIds { get; set; } = new List<int>();
    }
}
